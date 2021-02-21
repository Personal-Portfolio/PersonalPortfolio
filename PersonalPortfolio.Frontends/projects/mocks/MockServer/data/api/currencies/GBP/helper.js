module.exports = {
    isCorrectId: (data, id) => {
        const currency = JSON.parse(data);
    
        return typeof currency?.code === "string"
            && currency.code.toLowerCase() === id;
    }
}