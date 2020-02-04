//TODO: work for authorization
module.exports = (req, res, next) => {
    if (req.originalUrl === "/api/contacts/current") {
        res.sendStatus(403);
    } else {
        next();
    }
}