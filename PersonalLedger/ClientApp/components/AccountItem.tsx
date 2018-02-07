import * as React from 'react';
import { Account } from '../store/Accounts';
import { AccountEdit } from './AccountEdit';
import { Route, Link, RouteComponentProps } from 'react-router-dom';


export class AccountItem extends React.Component<{account: Account}, {}> {

    displayAsDollar = (amt: number) => '$ ' + amt.toFixed(2);
    displayAsPercent = (value: number) => value.toFixed(2) + "%";

    public render() {
        return <tr>
            <td>{this.props.account.id}</td>
            <td>{this.props.account.debit ? 'Asset' : 'Liability'}</td>
            <td>{this.props.account.name}</td>
            <td>{this.props.account.institution}</td>
            <td>{this.props.account.number}</td>
            <td className='right'>{this.displayAsPercent(this.props.account.interest)}</td>
            <td className='right'>{this.props.account.limit ? this.displayAsDollar(this.props.account.limit) : '--'}</td>
            <td className='right'>{this.displayAsDollar(this.props.account.balance)}</td>
            <td><Link to='/accountEdit' />Edit</td>
            <td><a onClick={() => confirm('are you sure you want to delete this account?')}>Delete</a></td>
            <Route path='/accountEdit' component={AccountEdit} />
        </tr>;
    }
}

    //<td><button onClick={() => { this.props.deleteAccount(account.id) }}>Delete</button></td>
//            <Route path='/accountEdit' component={AccountEdit account={this.props.accounts[0]} />
