import React, { Component } from 'react';
import { NavLink } from 'reactstrap';
import { Link } from 'react-router-dom';

export class InventoryMovies extends Component {
    static displayName = InventoryMovies.name;

    constructor(props) {
        super(props);
        this.state = { rows: [], loading: true };
    }

    componentDidMount() {
        this.populateInventoryMoviesData();
    }

    renderTransferTable(rows) {
        console.log(rows);
        //console.log(new Intl.DateTimeFormat("en-US").format(rows[0].invoiceTime));
        return (
            <table className='table table-striped' aria-labelledby="tabelLabel">
                <thead>
                    <tr>
                        <th>
                            <NavLink tag={Link} className="text-dark" to="/invoice">Add</NavLink>
                        </th>
                        <th>Id</th>
                        <th>Date</th>
                        <th>FromStorage</th>
                        <th>ToStorage</th>
                    </tr>
                </thead>
                <tbody>
                    {rows.map(item =>
                        <tr key={item.id}>
                            <td><button onClick={() => this.deleteInventoryMoviesData(item.id)}>Delete</button></td>
                            <td>{item.id}</td>
                            <td>
                                {
                                    new Intl.DateTimeFormat("ru-RU", { year: "numeric", month: "2-digit", day: "2-digit", hour: "2-digit", minute: "2-digit" }).format(new Date(item.invoiceTime))
                                }
                            </td>
                            <td>{item.fromStorage.name}</td>
                            <td>{item.toStorage.name}</td>
                        </tr>
                    )}
                </tbody>
            </table>
        );
    }

    render() {
        let contents = this.state.loading
            ? <p><em>Loading...</em></p>
            : this.renderTransferTable(this.state.rows);

        return (
            <div>
                <h1 id="tabelLabel" >Inventory Movies</h1>
                <p>List all inventory movies.</p>
                {contents}
            </div>
        );
    }

    async populateInventoryMoviesData() {
        this.setState({ loading: true });
        const response = await fetch('api/inventory/getallmovies');
        if (response.ok) {
            console.log(response);
            const data = await response.json();
            this.setState({ rows: data, loading: false });
        }
    }


    deleteInventoryMoviesData = async (id) => {
        const response = await fetch('api/invoice/delete/' + id, { method: 'DELETE' });
        if (response.ok) {
            let idx = this.state.rows.findIndex(item => item.id == id);
            if (idx > -1) {
                console.log('find', id);
                this.state.rows.splice(idx, 1);
                this.setState({ rows: this.state.rows });
            }
        }
    }


}
