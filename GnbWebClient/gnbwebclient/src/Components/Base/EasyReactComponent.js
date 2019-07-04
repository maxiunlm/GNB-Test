import { Component } from 'react';

export default class EasyReactComponent extends Component {
    constructor(props) {
        super(props);

        this.onChange = this.onChange.bind(this);
        this.onIntChange = this.onIntChange.bind(this);
        this.onMoneyChange = this.onMoneyChange.bind(this);
        this.componentWillUnmount = this.componentWillUnmount.bind(this);
        this.dispose = this.dispose.bind(this);
        this.disposeSubObject = this.disposeSubObject.bind(this);
        this.isSubObjectReferenceDisposable = this.isSubObjectReferenceDisposable.bind(this);
    }

    onChange(event) {
        let value = event.target.value;
        let state = [];

        state[event.target.id] = value;
        this.setState(state);
        event.preventDefault();
    }

    onIntChange(event) {
        let value = parseInt(event.target.value);
        let state = [];

        state[event.target.id] = value || '';
        this.setState(state);
        event.preventDefault();
    }

    onMoneyChange(event) {
        let value = event.target.value;

        if (isNaN(Number(value + '0'))) {
            return;
        }

        let index = value.indexOf('.');
        let length = value.length;
        let diff = length - index;

        if (diff < length && diff > 2) {
            value = Math.round(Number(value) * 100) / 100;
        }

        let state = [];
        state[event.target.id] = value || '';

        this.setState(state);
        event.preventDefault();
    }

    componentWillUnmount() {
        this.dispose();
    }

    dispose() {
        let keys = Object.keys(this);

        keys.forEach(this.disposeSubObject.bind(this));
    }

    disposeSubObject(key, index, arr) {
        let subObjectReference = this[key];

        if (!!this.isSubObjectReferenceDisposable
            && typeof this.isSubObjectReferenceDisposable === 'function'
            && this.isSubObjectReferenceDisposable(subObjectReference)) {
            subObjectReference.dispose();
        }

        this[key] = null;
    }

    isSubObjectReferenceDisposable(subObjectReference) {
        return !!subObjectReference && !!subObjectReference.dispose && typeof subObjectReference.dispose === 'function';
    }
}