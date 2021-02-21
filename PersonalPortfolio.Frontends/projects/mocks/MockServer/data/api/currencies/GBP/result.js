function eval (data, context) {
    const path = require("path");
    const helper = require(path.join(context, './helper.js'))
    const error = {
        errors: [
            {
                "id": 1,
                "status": "Bad request",
                "code": 400,
                "title": "Invalid currency model",
                "detail": "Provided JSON is not a correct currency"
            }
        ]
    };

    return helper.isCorrectId(data, "gbp")
        ? JSON.parse(data)
        : error;
}

module.exports = eval(request.body, context);