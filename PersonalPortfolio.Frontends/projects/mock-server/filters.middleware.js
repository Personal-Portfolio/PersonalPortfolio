function print(obj) {
    var pCount = 0;
    if (obj == null || obj == undefined) {
        pCount--;
        return;
    }
    if (typeof obj != 'object') {
        pCount--;
        return;
    }
    for (const propName in obj) {
        if (typeof obj[propName] == 'function')
            continue;
        if (typeof obj[propName] == 'object') {
            pCount++;
            print(obj[propName]);
        } else {
            console.log(intend(pCount), `${propName} : ${obj[propName]}`)
        }
    }
}

function intend(count) {
    return ' '.repeat(count);
}

module.exports = (req, res, next) => {
    if (req.method === "POST" && req.originalUrl === "/api/filters") {
        print(req.body);
        let filters = req.body["claimFilters"];
        let paging = req.body["pageSettings"];
        let id = "";
        if(filters[0].reporter.isInternal) {
            id = "requests-" + filters[0].assignee.customerName + "-" + paging.pageSize;
        } else {
            id = "claims-" + filters[0].reporter.customerName + "-" + paging.pageSize;;
        }
        return res.jsonp(id);
    } else {
        next();
    }
}