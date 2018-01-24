import { fetch, addTask } from 'domain-task';
import { Action, Reducer, ActionCreator } from 'redux';
import { AppThunkAction } from './';

export interface Account {
    id: number;
    balance: number;
    debit: boolean;
    defaultAcct?: number;
    defaultAmt?: number;
    defaultCat?: number;
    institution: string;
    interest: number;
    limit?: number;
    name: string;
    number: string;
    owned: boolean;
}

export interface AccountState {
    isLoading: boolean;
    accounts: Account[];
}

const unloadedState: AccountState = { accounts: [], isLoading: false };

interface RequestAccountsAction { type: 'REQUEST_ACCOUNTS' }
interface ReceiveAccountsAction { type: 'RECEIVE_ACCOUNTS'; accounts: Account[] }
interface AddAccountAction { type: 'ADD_ACCOUNT'; account: Account }
interface DeleteAccountAction { type: 'DELETE_ACCOUNT'; id: number }
interface UpdateAccountAction { type: 'UPDATE_ACCOUNT'; account: Account }
interface UpdateDefaultsAction { type: 'UPDATE_ACCOUNT_DEFAULTS'; id: number; defaultAcct: number; defaultAmt: number; defaultCat: number }

type KnownAction = RequestAccountsAction | ReceiveAccountsAction | AddAccountAction | DeleteAccountAction | UpdateAccountAction | UpdateDefaultsAction;

export const actionCreators = {
    requestAccounts: (): AppThunkAction<KnownAction> => (dispatch, getState) => {
        // Only load data if it's not already loading
        if (getState().accounts.isLoading === false) {
            let fetchTask = fetch('api/Accounts/AccountsAsync')
                .then(response => response.json() as Promise<Account[]>)
                .then(data => { dispatch({ type: 'RECEIVE_ACCOUNTS', accounts: data });
                });

            addTask(fetchTask); // Ensure server-side prerendering waits for this to complete
            dispatch({ type: 'REQUEST_ACCOUNTS' });
        }
    },
    addAccount: (account: Account) => <AddAccountAction>{ type: 'ADD_ACCOUNT', account: account },
    deleteAccount: (userId: number) => <DeleteAccountAction>{ type: 'DELETE_ACCOUNT', id: userId },
    updateAccount: (account: Account) => <UpdateAccountAction>{ type: 'UPDATE_ACCOUNT', account: account },
    updateDefaults: (id: number, defaultAcct: number, defaultAmt: number, defaultCat: number) => <UpdateDefaultsAction>{ type: 'UPDATE_ACCOUNT_DEFAULTS', id: id, defaultAcct: defaultAcct, defaultAmt: defaultAmt, defaultCat: defaultCat }
};

export const reducer: Reducer<AccountState> = (state: AccountState, incomingAction: Action) => {
    const action = incomingAction as KnownAction;
    switch (action.type) {
        case 'REQUEST_ACCOUNTS':
            return { isLoading: true, accounts: state.accounts };
        case 'RECEIVE_ACCOUNTS':
            return { isLoading: false, accounts: action.accounts };
        case 'ADD_ACCOUNT':
            return { isLoading: state.isLoading, accounts: state.accounts };
        case 'DELETE_ACCOUNT':
            return { isLoading: state.isLoading, accounts: state.accounts };
        case 'UPDATE_ACCOUNT':
            return { isLoading: state.isLoading, accounts: state.accounts };
        case 'UPDATE_ACCOUNT_DEFAULTS':
            return { isLoading: state.isLoading, accounts: state.accounts };
        default:
            // The following line guarantees that every action has been covered by a case
            const exhaustiveCheck: never = action;
    }

    // For unrecognized actions (or in cases where actions have no effect), must return the existing state
    //  (or default initial state if none was supplied)
    return state || unloadedState;
};
