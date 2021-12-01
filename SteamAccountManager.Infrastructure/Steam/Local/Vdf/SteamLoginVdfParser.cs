﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using Gameloop.Vdf;
using Gameloop.Vdf.Linq;
using SteamAccountManager.Domain.Steam.Exception.Vdf;
using SteamAccountManager.Domain.Steam.Local.Logger;
using SteamAccountManager.Infrastructure.Steam.Local.Dto;

namespace SteamAccountManager.Infrastructure.Steam.Local.Vdf
{
    public class SteamLoginVdfParser : ISteamLoginVdfParser
    {
        private readonly ILogger _logger;

        public SteamLoginVdfParser(ILogger logger)
        {
            _logger = logger;
        }
        
        private void SetDtoProperty(LoginUserDto dto, VProperty vProperty)
        {
            string value = vProperty.Value.ToString();
                    
            switch (vProperty.Key)
            {
                case AccountKeys.AccountName:
                    dto.AccountName = value;
                    break;
                case AccountKeys.PersonaName:
                    dto.PersonaName = value;
                    break;
                case AccountKeys.RememberPassword:
                    dto.PasswordRemembered = Int32.Parse(value) > 0;
                    break;
                case AccountKeys.MostRecent:
                    dto.MostRecent = Int32.Parse(value) > 0;
                    break;
                case AccountKeys.Timestamp:
                    dto.Timestamp = value;
                    break;
            }
        }

        public List<LoginUserDto> ParseLoginUsers(string vdfContent)
        {
            List<LoginUserDto> loginUsersDto = new();
            try
            {
                var vRootProperty = VdfConvert.Deserialize(vdfContent);

                foreach (var vProperty in vRootProperty.Value.Children<VProperty>())
                {
                    var steamId = vProperty.Key;

                    LoginUserDto dto = new LoginUserDto()
                    {
                        SteamId = steamId
                    };

                    _logger.LogInformation(GetType().Name, $"SteamId: {steamId}");

                    foreach (var vChildProperty in vProperty.Value.Children<VProperty>())
                    {
                        SetDtoProperty(dto, vChildProperty);
                        Debug.WriteLine($"{vChildProperty.Key}: {vChildProperty.Value}");
                    }

                    loginUsersDto.Add(dto);
                }

                return loginUsersDto;
            }
            catch (Exception)
            {
                throw new InvalidVdfException();
            }
        }

        private static class AccountKeys
        {
            public const string AccountName = "AccountName";
            public const string PersonaName = "PersonaName";
            public const string RememberPassword = "RememberPassword";
            public const string MostRecent = "MostRecent";
            public const string Timestamp = "Timestamp";
        }
    }
}