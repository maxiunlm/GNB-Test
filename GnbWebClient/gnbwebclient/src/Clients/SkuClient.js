import ClientBase from './Base/ClientBase';

export default class SkuClient extends ClientBase {
    constructor(loginClient) {
        super(loginClient);

        this.listSkus = this.listSkus.bind(this);
        this.getSku = this.getSku.bind(this);
        this.fetch = this.fetch.bind(this);
        this.getSummaryUri = this.getSummaryUri.bind(this);
        this.verifyResponse = this.verifyResponse.bind(this);
        this.getRequestOptions = this.getRequestOptions.bind(this);
    }

    async listSkus() {
        let skus = await this.fetch(process.env.REACT_APP_skusWebapiEndpoint);

        return skus;
    }

    async getSku(sku) {
        let uri = this.getSummaryUri(sku);
        let skuInfo = await this.fetch(uri);

        return skuInfo;
    }

    getSummaryUri(sku) {
        return process.env.REACT_APP_summaryWebapiEndpoint + sku;
    }
}