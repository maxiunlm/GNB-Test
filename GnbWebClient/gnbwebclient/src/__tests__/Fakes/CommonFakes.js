it('Fake Test', () => { });

export const CommonFakes = {
    zero: 0,
    one: 1,
    once: 1,
    twice: 2,
    threeTimes: 3,
    firstIndex: 0,
    secondIndex: 1,
    thirdIndex: 2,
    emptyString: '',
    visible: '',
    invisible: 'hidden',
    faleSku: 'E2114',
    skuOption: {
        value: 'value'
    },
    emptySkuInfo: {
        total: 0,
        transactions: []
    },
    emptyEvent: new Event(typeof (Event.toString())),
    emptyArray: [],
    oneOption: ['A1494'],
    twoOptions: ['B2038', 'B3587'],
    oneSku: ['A1257'],
    twoSkus: ['B7474', 'D8423'],
    oneRate: [{ "from": "EUR", "to": "CAD", "rate": 1.366 }],
    twoRates: [{ "from": "CAD", "to": "EUR", "rate": 0.732 }, { "from": "CAD", "to": "USD", "rate": 0.994788 }],
    oneTransaction: [{ "sku": "A1257", "currency": "AUD", "amount": 26.8 }],
    twoTransactions: [{ "sku": "A1257", "currency": "AUD", "amount": 23.7 }, { "sku": "A1257", "currency": "AUD", "amount": 18.8 }],
    skuInfo: { "name": "B2038", "total": 0.0, "transactions": [] },
    requestOptions: {
        method: process.env.REACT_APP_methodGet,
        headers: new Headers({
            'Content-Type': process.env.REACT_APP_applicationJson
        })
    },
    responseOk: {
        ok: true
    },
    responseBad: {
        ok: false
    }
};