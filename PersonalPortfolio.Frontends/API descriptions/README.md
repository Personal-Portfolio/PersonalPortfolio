# API versionning

## Why

One of the major challenges surrounding exposing services is handling updates to the API contract.

1. Clients may not want to update their applications when the API changes, so a versioning strategy becomes crucial. A versioning strategy allows clients to continue using the existing REST API and migrate their applications to the newer API when they are ready.
2. Versioning helps to iterate faster when the needed changes are identified.
3. API consumption should be as little impacted by changes in the API landscape as possible, and versioning helps to identify this impact.

In the real world, an API is never going to be completely stable. So it’s important how these changes are managed. A well documented and gradual deprecation of API can be an acceptable practice for most of the APIs.

## How

There are several common ways to version a REST API. Here it is needed to admit, that there is no _standartized_ way to dot it, all these ways are just a common practices.

1. Versioning through URI
    1. Versioning through path (e.g. ```some.api.com/api/v1/roles```)
    2. Versioning through domain (e.g. ```v1.some.api.com/api/roles```)
2. Versioning through query parameters (e.g. ```some.api.com/api/roles?version=1```, ```some.api.com/api/roles?version=1.2```, ```some.api.com/api/roles?version=1.2.3```, etc)
3. Versioning through custom headers (e.g. ```Accept-version: v1```, ```Accept-version: 2.0.0```, etc)
4. Vaiation of combinations

All these ways are compatible with [Semantic versioning](https://semver.org/) so it is better to use this approach too.

## Pros and cons

1. Versioning through URI
    - Pros: Clients can cache resources easily
    - Cons: This solution has a pretty big footprint in the code base as introducing breaking changes implies branching the entire API
2. Versioning through query parameters
    - Pros: It’s a straightforward way to version an API, and it’s easy to default to the latest version
    - Cons: Query parameters are more difficult to use for routing requests to the proper API version
3. Versioning through custom headers
    - Pros: It doesn’t clutter the URI with versioning information
    - Cons: It requires custom headers
