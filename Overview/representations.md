# Representations

The REST API is carefully designed to make not just PUTs, but also POSTs, strongly idempotent. POST typically does not carry an idempotency guarantee, but such a guarantee is extremely useful for distriuted systems.

## GET mathematicians/new

To create a mathematician, the application first issues a GET for a mathematician resource. A unique mathematician will be generated.

```json
{
    "unique": "4e5f8dff-bdd8-48d9-9c10-4eab38d0fab3",
    "name": {
        "ids": [],
        "firstName": null,
        "lastName": null
    },
    "_links": {
        "self": {
            "href": "http://localhost:4409/api/mathematicians/4e5f8dff-bdd8-48d9-9c10-4eab38d0fab3"
        }
    }
}
```

As GET guarantees, this action does not modify the system. No record is actually created. This GET simply generates a new `unique` GUID.

## POST mathematicians

After the application gets a new unique mathematician, it fills in the `firstName` and `lastName` and POSTs it to the collection. This action creates the mathematician and sets its name. The POST returns the newly created mathematician with a base-64 encoded hash in the `ids` array.

```json
{
    "unique": "4e5f8dff-bdd8-48d9-9c10-4eab38d0fab3",
    "name": {
        "ids": ["nnqhwfVkOmSjZ2u5ssH9wU4r1b516s0IsegWOKmug8o="],
        "firstName": "Leonhardt",
        "lastName": "Euler"
    },
    "_links": {
        "self": {
            "href": "http://localhost:4409/api/mathematicians/4e5f8dff-bdd8-48d9-9c10-4eab38d0fab3"
        }
    }
}
```

Because the client retrieved a unique ID before beginning this process, it can safely retry the POST if it fails. Unlike the typical REST API, this POST is idempotent.

## PUT mathematicians/{unique}

To change the name of a mathematician, the client modifies the `firstName` and `lastName` properties, and then PUTs the representation back to the URL. It is important that the client preserves the `ids` that were returned in the previous response. These indicate the current versions of the mutable property, so that the PUT can replace those specific versions. The server responds with the representation containing new `ids`.

```json
{
    "unique": "4e5f8dff-bdd8-48d9-9c10-4eab38d0fab3",
    "name": {
        "ids": ["u+eSWBxzqKy0SMrRoSHbL/wbqFbloHFpDq5Qm1jzwwM="],
        "firstName": "Leonard",
        "lastName": "Euler"
    },
    "_links": {
        "self": {
            "href": "http://localhost:4409/api/mathematicians/4e5f8dff-bdd8-48d9-9c10-4eab38d0fab3"
        }
    }
}
```

Why do you suppose the `ids` property is an array? Under what circumstances will it contain more than one hash code? What would happen if the client does not send back the `ids`?