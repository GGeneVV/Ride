using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace RidePal.Tests
{
    public class Utils
    {
        private static IMapper mapper;

        public static DbContextOptions<DbContext> GetOptions(string databaseName)
        {
            return new DbContextOptionsBuilder<DbContext>()
                .UseInMemoryDatabase(databaseName)
                .Options;
        }


        public static IMapper Mapper
        {
            get
            {
                if (mapper == null)
                {
                    // mapper = new Mapper(mapperConfig);
                }

                return mapper;
            }
        }




    }
}
