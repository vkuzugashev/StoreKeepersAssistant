import React, { Component } from 'react';

export class Invoice extends Component {
    static displayName = Invoice.name;

    constructor(props) {
        super(props);
        this.state = {
            id: 0,
            invoiceNumber: '',
            invoiceTime: null,
            storageId: null,
            fromStorageId: null,
            toStorageId: null,
            storageOptions: [],
            itemOptions: [],
            invoiceItems: [],
            invoiceItemId: null,
            itemId: '',
            itemQty: 0,
            loading: true
        };
    }

    componentDidMount() {
        this.load();
    }

    renderForm() {
        let options = this.state.storageOptions instanceof Array ? this.state.storageOptions.map((item, i) => { return (<option key={i} value={item.id}>{item.name}</option>) }) : '';
        let itemOptions = this.state.itemOptions instanceof Array ? this.state.itemOptions.map((item, i) => { return (<option key={i} value={item.id}>{item.name}</option>) }) : '';
        return (
            <>
                <div className='row'>
                    <div className='col-12'>

                        <label htmlFor='invoiceNumber'>InvoiceNumber</label>
                        <input type='text' value={this.state.invoiceNumber} onChange={(e) => { this.setState({ invoiceNumber: e.target.value }) }} />

                        <label htmlFor='fromStorage'>FromStorage</label>
                        <select value={this.state.fromStorageId ?? ''} onChange={(e) => { this.setState({ fromStorageId: e.target.value }) }}>
                            {options}
                        </select>

                        <label htmlFor='toStorage'>ToStorage</label>
                        <select value={this.state.toStorageId ?? ''} onChange={(e) => { this.setState({ toStorageId: e.target.value }) }}>
                            {options}
                        </select>
                    </div>

                </div>
                <div className='row'>
                    <div className='col-12'>
                        <h4>Invoice Items</h4>
                        <input value={this.state.invoiceItemId ?? ''} readOnly />
                        <select value={this.state.itemId ?? "001"} onChange={(e) => { this.setState({ itemId: e.target.value }) }}>
                            {itemOptions}
                        </select>
                        <input value={this.state.itemQty ?? 0} onChange={(e) => { this.setState({ itemQty: e.target.value }) }} />
                        <button onClick={this.invoiceItemAddOnClick}>Create New Item</button>
                        <button onClick={this.invoiceItemSaveOnClick}>Save</button>
                    </div>
                </div>
            </>
        );
    }

    renderInvoiceItemsTable(rows) {
        return (
            <table className='table table-striped' aria-labelledby="tabelLabel">
                <thead>
                    <tr>
                        <th>
                            
                        </th>
                        <th>Id</th>
                        <th>Item</th>
                        <th>Qoantity</th>
                    </tr>
                </thead>
                <tbody>
                    {rows.map(item =>
                        <tr key={item.id}>
                            <td>
                                <button onClick={() => this.invoiceItemSelectOnClick(item.id)}>Select {item.id}</button>
                                <button onClick={() => this.invoiceItemDeleteOnClick(item.id)}>Delete</button>
                            </td>
                            <td>{item.id}</td>
                            <td>{item.itemId}</td>
                            <td>{item.qty}</td>
                        </tr>
                    )}
                </tbody>
            </table>
        );
    }

    render() {
        let contents = this.state.loading
            ? <p><em>Loading...</em></p>
            : this.renderForm();
        let invoiceItemsTableContents = this.state.loading || !this.state.invoiceItems instanceof Array
            ? <div></div>
            : this.renderInvoiceItemsTable(this.state.invoiceItems);

        return (
            <div>
                <h1 id="tabelLabel">Invoice</h1>
                <p>Create new or update exists Invoice.</p>
                <label>InvoiceId</label>
                <input type='text' value={this.state.id} onChange={(e) => { this.setState({ id: e.target.value }) }} />
                <button onClick={this.invoiceLoadOnClick}>Load</button>
                <button onClick={this.invoiceAddOnClick}>New</button>
                <button onClick={this.invoiceSaveOnClick}>Save</button>
                {contents}
                {invoiceItemsTableContents}
            </div>
        );
    }

    load = async () => {
        let response = await fetch('api/storage');
        if (response.ok) {
            let data = await response.json();
            this.setState({ storageOptions: data });
            console.log(this.state.storageOptions);

            response = await fetch('api/item');
            if (response.ok) {
                data = await response.json();
                this.setState({ itemOptions: data });
                console.log(this.state.itemOptions);
            }
        }
    }

    invoiceLoadOnClick = async () => {
        let response = await fetch('api/invoice/' + this.state.id);
        if (response.ok) {
            let data = await response.json();
            this.setState({ id: data.id, invoiceNumber: data.invoiceNumber, invoiceTime: data.invoiceTime, storage: null, toStorage: null, loading: false });

            response = await fetch('api/invoiceItem/' + this.state.id);
            if (response.ok) {
                data = await response.json();
                this.setState({ invoiceItems: data, loading: false });
            }
        }
    }

    invoiceAddOnClick = async () => {
        this.setState({ id: 0, invoiceNumber: '', invoiceTime: null, loading: false });
    }

    invoiceSaveOnClick = async () => {
        if (this.state.id == 0) {
            const response = await fetch('api/invoice/create',
                {
                    method: 'POST',
                    headers: {
                        'Accept': 'application/json',
                        'Content-Type': 'application/json'
                    },
                    body: JSON.stringify({
                        id: this.state.id,
                        invoiceNumber: this.state.invoiceNumber,
                        invoiceTime: this.state.invoiceTime,
                        storage: { id: this.state.storageId },
                        toStorage: { id: this.state.toStorageId },
                        fromStorage: { id: this.state.fromStorageId },

                    })
                }
            );
            if (response.ok) {
                const data = await response.json();
                this.setState({ id: data.id, invoiceNumber: data.invoiceNumber, invoiceTime: data.invoiceTime, loading: false });
            }
        }
        else {
            const response = await fetch('api/invoice/update/' + this.state.id,
                {
                    method: 'PUT',
                    body: {}
                }
            );
            const data = await response.json();
            this.setState({ id: data.id, invoiceNumber: data.invoiceNumber, invoiceTime: data.invoiceTime, loading: false });
        }

        let path = 'Transfer';
        this.props.history.push(path);

    }

    invoiceDeleteOnClick = async () => {
        if (this.state.id != 0) {
            const response = await fetch('api/invoice/delete/' + this.state.id,
                {
                    method: 'DELETE',
                }
            );
            if (response.ok) {
                const data = await response.json();
                this.setState({ id: data.id, invoiceNumber: data.invoiceNumber, invoiceTime: data.invoiceTime, loading: false });
            }
        }
    }


    invoiceItemAddOnClick = async () => {
        this.setState({ invoiceItemId: 0, itemId: '', itemQty: 0 });
    }

    invoiceItemSaveOnClick = async () => {
        if (this.state.invoiceItemId == 0) {
            const response = await fetch('api/invoiceitem/create',
                {
                    method: 'POST',
                    headers: {
                        'Accept': 'application/json',
                        'Content-Type': 'application/json'
                    },
                    body: JSON.stringify({
                        id: this.state.invoiceItemId,
                        invoiceId: this.state.id,
                        itemId: this.state.itemId,
                        qty: this.state.itemQty,
                    })
                }
            )
            if (response.ok) {
                const data = await response.json();
                let arr = this.state.invoiceItems;
                arr.push(data);
                this.setState({
                    invoiceItemId: data.id,
                    invoiceId: data.invoiceId,
                    itemId: data.itemId,
                    itemQty: data.qty,
                    invoiceItems: arr,
                    loading: false
                });
            }
        }
        else {
            const response = await fetch('api/invoiceitem/update/' + this.state.invoiceItemId,
                {
                    method: 'PUT',
                    headers: {
                        'Accept': 'application/json',
                        'Content-Type': 'application/json'
                    },
                    body: JSON.stringify({})
                }
            );
            const data = await response.json();
            this.setState({ invoiceItemId: data.id, invoiceId: data.invoiceId, itemId: data.itemId, itemQty: data.qty, loading: false });
        }

    }

    invoiceItemDeleteOnClick = async (id) => {
        console.log('invoiceItemDeleteOnClick', id);
        if (id != 0) {
            const response = await fetch('api/invoiceItem/delete/' + id, { method: 'DELETE' });
            if (response.ok) {
                console.log('success delete', id);
                let idx = this.state.invoiceItems.findIndex(item => item.id == id);
                if (idx > -1) {
                    console.log('find', id);
                    this.state.invoiceItems.splice(idx, 1);
                    this.setState({ invoiceItems: this.state.invoiceItems });
                }
            }
        }
        this.setState({ invoiceItemId: 0, itemId: 0, itemQty: 0, loading: false });
    }

    invoiceItemSelectOnClick = async (id) => {
        console.log('invoiceItemSelectOnClick', id);
        if (id != 0) {
            let item = this.state.invoiceItems.find(item => item.id == id);
            if (item) {
                this.setState({ invoiceItemId: item.id, itemId: item.itemId, itemQty: item.itemQty });
            }
        }
    }
}

