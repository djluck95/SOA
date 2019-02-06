import axios from 'axios';
import * as React from 'react';

interface Order {
    id: string;
    name: string;
    quantity: string;
    date: string;
}

interface Props { }
interface State {
    orders: Order[];
    isLoading: boolean;

    createOrder: Order;
}

export default class Root extends React.Component<Props, State> {
    constructor(props: Props) {
        super(props);

        this.state = {
            orders: [],
            isLoading: false,
            createOrder: {
                id: '',
                name: '',
                quantity: '',
                date: ''
            }
        };
    }

    createOrder() {
        axios.post('http://localhost:50021/OrderService.svc/PutOrder', this.state.createOrder)
            .then(() => {
                // reset form
                this.setState({
                    createOrder: {
                        id: '',
                        date: '',
                        name: '',
                        quantity: ''
                    }
                });

                alert('Order placed');
            });
    }

    fetchOrders() {
        this.setState({ isLoading: true });

        axios.get('http://localhost:50021/OrderService.svc/GetOrders').then(response => {
            console.log('/GetOrders response is ', response);
            this.setState({
                isLoading: false,
                orders: response.data
            });
        }).catch(() => {
            this.setState({ isLoading: false });
            alert('Fetch error');
        });
    }

    deleteOrder(id: string) {
        axios.delete('http://localhost:50021/OrderService.svc/DeleteOrder', { data: { id: id } }).then(() => alert('Order Deleted'));
    }

    componentWillMount() {
        this.fetchOrders();
    }

    render() {
        return (
            <div>
                <h1>Add Order</h1>
                <input
                    type="text"
                    placeholder="Name"
                    value={this.state.createOrder.name}
                    onChange={(e) => {
                        const v = e.currentTarget.value;

                        this.setState(prevState => ({
                            createOrder: {
                                ...prevState.createOrder,
                                name: v,
                                id:'1'
                            }
                        }));
                    }}
                />
                <br />
                <br />
                <input
                    type="text"
                    placeholder="Quantity"
                    value={this.state.createOrder.quantity}
                    onChange={(e) => {
                        const v = e.currentTarget.value;

                        this.setState(prevState => ({
                            createOrder: {
                                ...prevState.createOrder,
                                quantity: v
                            }
                        }));
                    }}
                />
                <br />
                <br />
                <input
                    type="text"
                    placeholder="Date"
                    value={this.state.createOrder.date}
                    onChange={(e) => {
                        const v = e.currentTarget.value;

                        this.setState(prevState => ({
                            createOrder: {
                                ...prevState.createOrder,
                                date: v
                            }
                        }));
                    }}
                />
                <br />
                <br />
                <button onClick={() => this.createOrder()}>Order</button>
                <hr />
                {this.state.isLoading && 'Loading...'}
                <table>
                    <thead>
                        <tr>
                            <th>Id</th>
                            <th>Name</th>
                            <th>Quantity</th>
                            <th>Date</th>
                            <th>Actions</th>
                        </tr>
                    </thead>
                    <tbody>
                        {
                            this.state.orders.map(order => (
                                <tr key={order.id}>
                                    <td>{order.id}</td>
                                    <td>{order.name}</td>
                                    <td>{order.quantity}</td>
                                    <td>{order.date}</td>
                                    <td>
                                        <button onClick={() => this.deleteOrder(order.id)}>
                                            Delete
                                        </button>
                                    </td>
                                </tr>
                            ))
                        }
                    </tbody>
                </table>
            </div>
        );
    }
}