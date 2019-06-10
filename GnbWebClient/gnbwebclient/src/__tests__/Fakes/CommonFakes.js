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
    skuOption: {
        value: 'value'
    },
    emptySkuInfo: {
        total: 0,
        transactions: []
    },
    emptyEvent: new Event(typeof (Event.toString())),
    emptyArray: [],
    oneOption: ['option 1'],
    twoOptions: ['option 1', 'options 2'],
    oneRate: [{ "from": "EUR", "to": "CAD", "rate": 1.366 }],
    twoRates: [{ "from": "CAD", "to": "EUR", "rate": 0.732 }, { "from": "CAD", "to": "USD", "rate": 0.994788 }]
};