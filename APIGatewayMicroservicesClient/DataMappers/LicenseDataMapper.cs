using APIGatewayEntities.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace LicenseProxyAPI.DataMappers
{
    public static class LicenseDataMapper
    {
        private const int HOURS_IN_DAY = 24;
        public static int ToNumberOfHours(this LicenseDuration? licenseDuration)
        {
            if (licenseDuration == null)
            { 
                return 0; 
            }

            if (licenseDuration == LicenseDuration.Unknown)
            {
                return 0;
            }

            switch (licenseDuration)
            {
                case LicenseDuration.OneDay: return HOURS_IN_DAY;

                case LicenseDuration.TwoDays: return HOURS_IN_DAY*2;
                case LicenseDuration.ThreeDays: return HOURS_IN_DAY*3;
                case LicenseDuration.Week: return HOURS_IN_DAY*7;
                case LicenseDuration.Month: return HOURS_IN_DAY*30; //TODO: Change to thirty days???

                default: return 0;
            }
        }

        public static ContentLicense ToContentLicense(this ContentLicenseDto model)
        {
            return new ContentLicense
            {
                Uuid = model.Uuid,
                UserId = model.UserId,
                ContentId = model.ContentId,
                LicenseRules = model.LicenseRules,
                LicenseStatus = model.LicenseStatus,
                ActivationTime = model.ActivationTime,
            };
        }

        public static ContentLicenseDto ToContentLicenseDto(this ContentLicense entity)
        {
            return new ContentLicenseDto
            {
                Uuid = entity.Uuid,
                UserId = entity.UserId,
                ContentId = entity.ContentId,
                LicenseRules = entity.LicenseRules,
                LicenseStatus = entity.LicenseStatus,
                ActivationTime = entity.ActivationTime,
            };
        }
    }

}
