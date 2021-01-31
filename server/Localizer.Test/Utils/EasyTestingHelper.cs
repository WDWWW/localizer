using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using Hestify;
using Localizer.Domain;
using Microsoft.EntityFrameworkCore;
using Wd3w.AspNetCore.EasyTesting;
using Wd3w.AspNetCore.EasyTesting.Hestify;

namespace Localizer.Test.Utils
{
	public static class EasyTestingHelper
	{
		public static SystemUnderTest VerifyEntityExistsByCondition<TEntity>(this SystemUnderTest sut, Expression<Func<TEntity, bool>> condition) where TEntity : class
		{
			sut.UsingService((LocalizerDb db) => db.Set<TEntity>().Any(condition).Should().BeTrue());
			return sut;
		}

		public static SystemUnderTest VerifyEntity<TEntity>(this SystemUnderTest sut, Action<DbSet<TEntity>> verifier) where TEntity : class
		{
			sut.UsingService((LocalizerDb db) => verifier(db.Set<TEntity>()));
			return sut;
		}

		public static TEntity FindEntity<TEntity>(this SystemUnderTest sut, object key) where TEntity : class
		{
			TEntity? entity = default;
			sut.UsingService((LocalizerDb db) => entity = db.Set<TEntity>().Find(key));
			return entity ?? throw new KeyNotFoundException($"Couldn't find any entity(Type: {typeof(TEntity).Name}) for Key({key}).");
		}

		public static Task<HttpResponseMessage> GetAsync(this SystemUnderTest sut,
			string resource,
			(string key, string value)[]? parameter = default,
			string? accessToken = default)
		{
			return SetupClient(sut, resource, default, parameter, accessToken).GetAsync();
		}

		public static Task<HttpResponseMessage> DeleteAsync(this SystemUnderTest sut,
			string resource,
			(string key, string value)[]? parameter = default,
			string? accessToken = default)
		{
			return SetupClient(sut, resource, default, parameter, accessToken).DeleteAsync();
		}

		public static Task<HttpResponseMessage> PostAsync(this SystemUnderTest sut, string resource,
			object? body = default, 
			(string key, string value)[]? parameter = default,
			string? accessToken = default
		)
		{
			return SetupClient(sut, resource, body, parameter, accessToken).PostAsync();
		}

		public static Task<HttpResponseMessage> PatchAsync(this SystemUnderTest sut,
			string resource,
			object? body = default,
			(string key, string value)[]? parameter = default,
			string? accessToken = default)
		{
			return SetupClient(sut, resource, body, parameter, accessToken).PatchAsync();
		}

		public static async Task<HttpResponseMessage> PutAsync(this SystemUnderTest sut,
			string resource,
			object? body = default,
			(string key, string value)[]? parameter = default,
			string? accessToken = default)
		{
			return await SetupClient(sut, resource, body, parameter, accessToken).PutAsync();
		}

		public static Task<TResponse> GetAsync<TResponse>(this SystemUnderTest sut,
			string resource,
			(string key, string value)[]? parameter = default,
			string? accessToken = default)
		{
			return ReadResponseBody<TResponse>(SetupClient(sut, resource, default, parameter, accessToken).GetAsync());
		}

		public static Task<TResponse> DeleteAsync<TResponse>(this SystemUnderTest sut,
			string resource,
			(string key, string value)[]? parameter = default,
			string? accessToken = default)
		{
			return ReadResponseBody<TResponse>(SetupClient(sut, resource, default, parameter, accessToken).DeleteAsync());
		}

		public static Task<TResponse> PostAsync<TResponse>(this SystemUnderTest sut,
			string resource,
			object? body = default,
			(string key, string value)[]? parameter = default,
			string? accessToken = default)
		{
			return ReadResponseBody<TResponse>(SetupClient(sut, resource, body, parameter, accessToken).PostAsync());
		}

		public static Task<TResponse> PatchAsync<TResponse>(this SystemUnderTest sut,
			string resource,
			object? body = default,
			(string key, string value)[]? parameter = default,
			string? accessToken = default)
		{
			return ReadResponseBody<TResponse>(SetupClient(sut, resource, body, parameter, accessToken).PatchAsync());
		}

		private static async Task<TResponse> ReadResponseBody<TResponse>(Task<HttpResponseMessage> patchAsync)
		{
			var message = await patchAsync;
			message.EnsureSuccessStatusCode();
			return await message.ReadJsonBodyAsync<TResponse>();
		}

		public static Task<TResponse> PutAsync<TResponse>(this SystemUnderTest sut,
			string resource,
			object? body = default,
			(string key, string value)[]? parameter = default,
			string? accessToken = default)
		{
			return ReadResponseBody<TResponse>(SetupClient(sut, resource, body, parameter, accessToken).PutAsync());
		}

		private static HestifyClient SetupClient(SystemUnderTest sut,
			string resource,
			object? body,
			(string key, string value)[]? parameter,
			string? accessToken)
		{
			var client = sut.Resource(resource);

			if (body != null)
				client = client.WithJsonBody(body);

			if (parameter != default)
				client = client.WithParams(parameter);

			if (accessToken != default)
				client = client.WithBearerToken(accessToken);
			
			return client;
		}
	}
}