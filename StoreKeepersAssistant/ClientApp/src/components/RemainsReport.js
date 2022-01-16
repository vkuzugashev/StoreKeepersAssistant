import React, { Component } from 'react';

export class RemainsReport extends Component {
    static displayName = RemainsReport.name;

    constructor(props) {
        super(props);
        this.state = { rows: [], loading: true };
    }

    componentDidMount() {
        this.populateRemainsData();
    }

    renderRemainsTable(rows) {
        return (
            <table className='table table-striped' aria-labelledby="tabelLabel">
                <thead>
                    <tr>
                        <th>Item</th>
                        <th>Qoantity</th>
                    </tr>
                </thead>
                <tbody>
                    {rows.map(item =>
                        <tr key={item.id}>
                            <td>{item.item.name}</td>
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
            : this.renderRemainsTable(this.state.rows);

        return (
            <div>
                <h1 id="tabelLabel" >All transfer</h1>
                <p>This component demonstrates fetching data from the server.</p>
                {contents}
            </div>
        );
    }

    async populateRemainsData() {
        this.setState({ loading: true });
        const response = await fetch('api/invoice');
        const data = await response.json();
        this.setState({ rows: data, loading: false });
    }

}
