﻿using Core.DataAccess.EntityFramework;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Linq.Expressions;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfRentalDal : EfEntityRepositoryBase<Rental, RentACarContext>, IRentalDal
    {
        public List<RentalDetailDto> GetRentalDetails()
        {
            using (RentACarContext context = new RentACarContext())
            {
                var result = from rental in context.Rentals
                    join car in context.Cars on rental.CarId equals car.CarId
                    join customer in context.Customers on rental.CustomerId equals customer.CustomerId
                    join user in context.Users on customer.UserId equals user.Id
                    join brand in context.Brands on car.BrandId equals brand.BrandId
                    select new RentalDetailDto()
                    {
                        RentalId = rental.RentalId,
                        CarDescription = car.Description,
                        CarBrand = brand.BrandName,
                        CarModel = car.ModelYear,
                        CompanyName = customer.CompanyName,
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        RentDate = rental.RentDate,
                        ReturnDate = rental.ReturnDate,
                        CarId = car.CarId,
                        UserId = user.Id
                    };
                return result.ToList();
            }
        }
    }
}
