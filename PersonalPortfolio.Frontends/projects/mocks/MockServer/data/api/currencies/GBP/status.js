function eval (data, context) {
    const path = require("path");
    const helper = require(path.join(context, './helper.js'))
    return helper.isCorrectId(data, "gbp")
        ? "HTTP/1.1 200 OK"
        : "HTTP/1.1 400 Bad request";
}

module.exports = eval(request.body, context);