﻿using AutoMapper;

namespace Setsis.Service.Mapping
{
    public static class ObjectMapper
    {
        public static IMapper Mapper => lazy.Value;

        private static readonly Lazy<IMapper> lazy = new Lazy<IMapper>(() =>
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<DtoMapper>();
            });

            return config.CreateMapper();
        });
    }
}
