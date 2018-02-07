import * as React from 'react';
import { Modal } from 'semantic-ui-react'
import { Link, RouteComponentProps } from 'react-router-dom';
import { connect } from 'react-redux';
import { ApplicationState } from '../store';
import * as AccountStore from '../store/Accounts';
import { Account } from '../store/Accounts';

type AccountAddProps =
    AccountStore.AccountState
    & typeof AccountStore.actionCreators
    & RouteComponentProps<{}>;

export class AccountAdd extends React.Component<AccountAddProps, {}> {

    public render() {
        var debit: boolean = true;
        const displayAsDollar = (amt: number) => '$ ' + amt.toFixed(2);
        const displayAsPercent = (value: number) => value.toFixed(2) + "%";
        const handleDebitButton = () => debit = !debit;

        return <Modal>
            <form>
                <label>Type</label>
                <label>Name</label>
                <label>Institution</label>
                <label>Number</label>
                <label>Interest Rate</label>
                <label>Limit</label>
                <br />
            </form>
        </Modal>;
    }
}

export default connect((state: ApplicationState) => state.accounts, AccountStore.actionCreators)(AccountAdd) as typeof AccountAdd;


//return <form>
//    <label>Type</label>
//    <input type='radio' value='true' checked={debit} onChange={handleDebitButton} /> Asset
//            <input type='radio' value='false' checked={!debit} onChange={handleDebitButton} /> Liability
//            <label>Name</label>
//    <input type='text' />
//    <label>Institution</label>
//    <input type='text' />
//    <label>Number</label>
//    <input type='text' />
//    <label>Interest Rate</label>
//    <input type='number' />%
//            <label>Limit</label>
//    $<input type='number' />
//    <input type='submit'>Add</input>
//</form>;

//                <label>DEBIT IS: {this.state.debit ? 'true' : 'false'} </label>

//export class AccountAdd extends React.Component<AccountAddProps, {
//    debit: boolean,
//    institution: string,
//    interest: number,
//    limit: number | null,
//    name: string,
//    number: string
//}> {

//    constructor() {
//        super();
//        this.state = {
//            debit: true,
//            institution: '',
//            interest: 0,
//            limit: null,
//            name: '',
//            number: ''
//        }
//    }
