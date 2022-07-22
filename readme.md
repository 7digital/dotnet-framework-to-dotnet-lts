# .NET Framework to .NET latest LTS

We have a simple API which is running in .NET Framework 4.7.1. We want to get this running in the latest .NET LTS on Linux, whilst replacing deprecated dependencies.

## Checkout API

The API has two endpoints:
 - Get an item (`GET ~/item/{code}`)
 - Checkout (`POST ~/checkout`)

The API responds in both XML and JSON (depending on the `Accept` header). It only accepts JSON for the `POST` endpoint.

### Things of note

There is full test coverage (happy and unhappy cases) using unit tests and acceptance tests. Test coverage must not be compromised.

The API uses Nancy, which is deprecated outside of .NET Framework.

Some of the code simulates asynchronous integrations, in place of talking to a real service or database. The behaviour of this code should not be changed.

Don't worry too much about what the code is actually doing and only refactor what's necessary to complete the migration.

If you want to run the .NET Framework acceptance tests, first run `setup.bat` in administrator mode first to setup the site in IIS.
