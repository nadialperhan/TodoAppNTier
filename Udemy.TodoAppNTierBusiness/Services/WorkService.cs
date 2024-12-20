﻿using AutoMapper;
using FluentValidation;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
//using System.Linq;
using System.Threading.Tasks;
using Udemy.TodoAppNTier.Common.ResponseObjects;
using Udemy.TodoAppNTier.DataAccess.UnitofWork;
using Udemy.TodoAppNTier.Dtos.FilterDto;
using Udemy.TodoAppNTier.Dtos.WorkDtos;
using Udemy.TodoAppNTier.Entities.Domains;
using Udemy.TodoAppNTierBusiness.Extensions;
using System.Linq.Dynamic.Core;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Udemy.TodoAppNTierBusiness.Services
{
    public class WorkService : IWorkService
    {
        private readonly IUow _uow;
        private readonly IMapper _mapper;
        private readonly IValidator<WorkCreateDto> _createdtovalidator;
        private readonly IValidator<WorkUpdatedDto> _updatedtovalidator;
        public WorkService(IUow uow, IMapper mapper, IValidator<WorkCreateDto> createdtovalidator, IValidator<WorkUpdatedDto> updatedtovalidator)
        {
            _uow = uow;
            _mapper = mapper;
            _createdtovalidator = createdtovalidator;
            _updatedtovalidator = updatedtovalidator;
        }
        public async Task<IResponse<WorkCreateDto>> Create(WorkCreateDto createDto)
        {
            var validationresult = _createdtovalidator.Validate(createDto);
            if (validationresult.IsValid)
            {
                await _uow.GetRepository<Work>().Create(_mapper.Map<Work>(createDto));
                await _uow.SaveChanges();
                return new Response<WorkCreateDto>(ResponseType.Success, createDto);
            }
            else
            {
                return new Response<WorkCreateDto>(ResponseType.ValidationError, createDto, validationresult.ConvertToCustomValidationError());
            }
        }

        //public Task<IResponse<List<WorkListDtos>>> DenemeNadi()
        //{
        //    throw new System.NotImplementedException();
        //}

        //public Task<IEnumerable<TResult>> ExecuteStoredProcedureAsync<TResult>(string storedProcedureName, params object[] parameters) where TResult : class
        //{
        //    return await _uow.GetRepository<TResult>().ExecuteStoredProcedureAsync(storedProcedureName, parameters);

        //}

        public async Task<IResponse<List<WorkListDtos>>> GetAll()
        {
            var data = _mapper.Map<List<WorkListDtos>>(await _uow.GetRepository<Work>().GetAll());
            return new Response<List<WorkListDtos>>(ResponseType.Success, data);
        }
        public async Task<IResponse<IDto>> GetbyID<IDto>(int id)
        {
            var data = _mapper.Map<IDto>(await _uow.GetRepository<Work>().GetByFilter(x => x.Id == id));
            if (data == null)
            {
                return new Response<IDto>(ResponseType.NotFound, $"{id} ye ait data bulunamadı");
            }
            return new Response<IDto>(ResponseType.Success, data);
        }

        public async Task<IResponse<List<WorkListDto>>> GetWorkDataUsingStoredProcedure(IndexFilterDto indexfilter)
        {
            //List<KeyValuePair<string, object>> parameters = new List<KeyValuePair<string, object>>();
            //parameters.Add(KeyValuePair.Create<string, object>("@Definition", "test"));
            //parameters.Add(KeyValuePair.Create<string, object>("@IsCompleted", true));
            string defini = string.Empty;
            bool? isComp = null;
            if (!string.IsNullOrEmpty(indexfilter.Definition))
            {
                defini = indexfilter.Definition;
            }
            if (indexfilter.IsCompleted!=null)
            {
                isComp = indexfilter.IsCompleted;
            }
            string sortcolumn = "Definition";
            List<SqlParameter> parms = new List<SqlParameter>
            {
          
              new SqlParameter { ParameterName = "@Definition", Value = defini },
              new SqlParameter { ParameterName = "@IsCompleted", Value = (object)isComp ?? DBNull.Value }
              //new SqlParameter { ParameterName = "@IsCompleted", Value = isComp }
            };

            //parameters.Add(KeyValuePair.Create<string, object>("@DbName", "nadi"));
            //parameters.Add(KeyValuePair.Create<string, object>("@UserName", "nadi"));
            var dbContext = _uow.GetDbContext(); // Replace with your logic to get DbContext

            //List<WorkListDto> results = await dbContext.ExecuteStoredProcedure<WorkListDto>("Deneme", parms);
            //List<WorkListDto> results = await dbContext.ExecuteStoredProcedure<WorkListDto>("sp_Deneme", parms);
            var results = await dbContext.ExecuteStoredProcedure<WorkListDto>("sp_Deneme", parms);
            //var resultsq =  dbContext.ExecuteStoredProcedureQueryable<WorkListDto>("sp_Deneme", parms);
            //var c=resultsq.OrderBy("Definition").ToListAsync();
            if (!String.IsNullOrEmpty(sortcolumn))
            {
                results = results.AsQueryable().OrderBy("Definition").ToList();
                //results= results.OrderBy("Definition").ToListAsync();
            }
            return new Response<List<WorkListDto>>(ResponseType.Success, results);
            //return results;
        }

        public async Task<IResponse> Remove(int id)
        {
            var removedEntity = await _uow.GetRepository<Work>().GetByFilter(x => x.Id == id);
            if (removedEntity != null)
            {
                _uow.GetRepository<Work>().Remove(removedEntity);
                await _uow.SaveChanges();
                return new Response(ResponseType.Success);
            }
            return new Response(ResponseType.NotFound, $"{id} ye ait data bulunamadı");
        }
        public async Task<IResponse<WorkUpdatedDto>> Update(WorkUpdatedDto dto)
        {
            var result = _updatedtovalidator.Validate(dto);
            if (result.IsValid)
            {
                var updatedEntity = await _uow.GetRepository<Work>().Find(dto.Id);
                if (updatedEntity != null)
                {
                    _uow.GetRepository<Work>().Update(_mapper.Map<Work>(dto), updatedEntity);
                    await _uow.SaveChanges();
                    return new Response<WorkUpdatedDto>(ResponseType.Success, dto);
                }
                return new Response<WorkUpdatedDto>(ResponseType.NotFound, "Bulunamadı");
            }
            else
            {
                return new Response<WorkUpdatedDto>(ResponseType.ValidationError, dto, result.ConvertToCustomValidationError());
            }
        }

       
        //public async Task<List<WorkListDto>> GetWorkDataUsingStoredProcedure()
        //{
        //    List<KeyValuePair<string, object>> parameters = new List<KeyValuePair<string, object>>();
        //    parameters.Add(KeyValuePair.Create<string, object>("@DbName", "nadi"));
        //    parameters.Add(KeyValuePair.Create<string, object>("@UserName", "nadi"));
        //    var dbContext = _uow.GetDbContext(); // Replace with your logic to get DbContext

        //    var results = await dbContext.ExecuteStoredProcedure<WorkListDto>("a", parameters);

        //    return results;
        //}


    }
}
