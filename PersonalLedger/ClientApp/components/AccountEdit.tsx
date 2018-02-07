import * as React from 'react';
import { PropTypes } from 'react';
import { Link, RouteComponentProps } from 'react-router-dom';
import { Modal } from 'semantic-ui-react'
import { connect } from 'react-redux';
import { ApplicationState } from '../store';
import * as AccountStore from '../store/Accounts';
import { Account } from '../store/Accounts';


type AccountEditProps =
    AccountStore.AccountState
    & typeof AccountStore.actionCreators
    & RouteComponentProps<{}>;

export class AccountEdit extends React.Component<AccountEditProps, {}> {

    displayAsDollar = (amt: number) => '$ ' + amt.toFixed(2);
    displayAsPercent = (value: number) => value.toFixed(2) + "%";

    public render() {
        return<Modal>
            <form>
                <label>Type</label>
                <label>Name</label>
                <label>Institution</label>
                <label>Number</label>
                <label>Interest Rate</label>
                <label>Limit</label>
            </form>
        </Modal>;
    }
}

export default connect((state: ApplicationState) => state.accounts, AccountStore.actionCreators)(AccountEdit) as typeof AccountEdit;

//return <form>
//    <input type='hidden' value={this.props.account.id} />
//    <label>Type</label>
//    <input type='radio' value='true' checked={this.props.account.debit} onChange={this.handleDebitButton} /> Asset
//            <input type='radio' value='false' checked={!this.props.account.debit} onChange={this.handleDebitButton} /> Liability
//            <label>Name</label>
//    <input type='text' value={this.props.account.name} />
//    <label>Institution</label>
//    <input type='text' value={this.props.account.institution} />
//    <label>Number</label>
//    <input type='text' value={this.props.account.number} />
//    <label>Interest Rate</label>
//    <input type='number' value={this.props.account.interest} />%
//            <label>Limit</label>
//    $<input type='number' value={this.props.account.limit} />
//    <input type='submit'>Update</input>
//</form>;
//<button onClick={() => { this.props.updateAccount(this.props.account) }}>Edit</button>

//    handleDebitButton = () => this.props.account.debit = !this.props.account.debit;
