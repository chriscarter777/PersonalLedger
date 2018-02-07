import { fetch, addTask } from 'domain-task';
import { Action, Reducer, ActionCreator } from 'redux';
import { AppThunkAction } from './';

export interface Transaction {
    id: number;
    amount: number;
    category: number;
    crAcct: number;
    date: string;
    drAcct: number;
    tax: boolean;
}

export interface TransactionState {
    isLoading: boolean;
    transactions: Transaction[];
}

const unloadedState: TransactionState = { transactions: [], isLoading: false };

interface RequestTransactionsAction { type: 'REQUEST_TRANSACTIONS' }
interface ReceiveTransactionsAction { type: 'RECEIVE_TRANSACTIONS'; transactions: Transaction[] }
interface AddTransactionAction { type: 'ADD_TRANSACTION'; transaction: Transaction }
interface DeleteTransactionAction { type: 'DELETE_TRANSACTION'; id: number; }
interface UpdateTransactionAction { type: 'UPDATE_TRANSACTION'; transaction: Transaction }

type KnownAction = RequestTransactionsAction | ReceiveTransactionsAction | AddTransactionAction | DeleteTransactionAction | UpdateTransactionAction;

export const actionCreators = {
    requestTransactions: (): AppThunkAction<KnownAction> => (dispatch, getState) => {
        // Only load data if it's not already loading
        if (getState().transactions.isLoading === false) {
            let fetchTask = fetch('api/Transactions/TransactionsAsync', { credentials: 'same-origin' })
                .then(response => response.json() as Promise<Transaction[]>)
                .then(data => {
                    dispatch({ type: 'RECEIVE_TRANSACTIONS', transactions: data });
                });

            addTask(fetchTask); // Ensure server-side prerendering waits for this to complete
            dispatch({ type: 'REQUEST_TRANSACTIONS' });
        }
    },
    addTransaction: (transaction: Transaction) => <AddTransactionAction>{ type: 'ADD_TRANSACTION', transaction: transaction },
    deleteTransaction: (id: number) => <DeleteTransactionAction>{ type: 'DELETE_TRANSACTION', id: id },
    updateTransaction: (transaction: Transaction) => <UpdateTransactionAction>{ type: 'UPDATE_TRANSACTION', transaction: transaction }
};

export const reducer: Reducer<TransactionState> = (state: TransactionState, incomingAction: Action) => {
    const action = incomingAction as KnownAction;
    switch (action.type) {
        case 'REQUEST_TRANSACTIONS':
            return { isLoading: true, transactions: state.transactions };
        case 'RECEIVE_TRANSACTIONS':
            return { isLoading: false, transactions: action.transactions  };
        case 'ADD_TRANSACTION':
            return { isLoading: state.isLoading, transactions: state.transactions };
        case 'DELETE_TRANSACTION':
            return { isLoading: state.isLoading, transactions: state.transactions };
        case 'UPDATE_TRANSACTION':
            return { isLoading: state.isLoading, transactions: state.transactions };
        default:
            // The following line guarantees that every action has been covered by a case
            const exhaustiveCheck: never = action;
    }

    return state || unloadedState;
};
