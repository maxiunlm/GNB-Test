import ClientBase from './Base/ClientBase';

const summaryWebapiEndpoint = 'http://localhost:5000/api/Sku/summary/';
const skusWebapiEndpoint = 'http://localhost:5000/api/Sku';

export default class SkuClient extends ClientBase {
    constructor() {
        super();

        this.listSkus = this.listSkus.bind(this);
        this.getSku = this.getSku.bind(this);
        this.fetch = this.fetch.bind(this);
        this.getSummaryUri = this.getSummaryUri.bind(this);
        this.verifyResponse = this.verifyResponse.bind(this);
        this.getRequestOptions = this.getRequestOptions.bind(this);
    }

    async listSkus() {
        let skusInfo = await this.fetch(skusWebapiEndpoint);

        return skusInfo;
    }

    async getSku(sku) {
        let uri = this.getSummaryUri(sku);
        let skuInfo = await this.fetch(uri);

        return skuInfo;
    }

    getSummaryUri(sku) {
        return summaryWebapiEndpoint + sku;
    }
}