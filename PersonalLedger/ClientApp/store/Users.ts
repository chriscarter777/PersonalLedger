import { fetch, addTask } from 'domain-task';
import { Action, Reducer, ActionCreator } from 'redux';
import { AppThunkAction } from './';

interface User {
    admin: boolean;
    id: string;
    userName: string
}

export interface UserState {
    isLoading: boolean;
    users: User[];
}

const unloadedState: UserState = { users: [], isLoading: false };

interface RequestUsersAction { type: 'REQUEST_USERS' }
interface ReceiveUsersAction { type: 'RECEIVE_USERS'; users: User[] }
interface AddUserAction { type: 'ADD_USER'; user: User }
interface DeleteUserAction { type: 'DELETE_USER'; id: string }
interface ToggleAdminAction { type: 'TOGGLE_ADMIN'; id: string }
//interface MakeAdminAction { type: 'MAKE_ADMIN'; id: string }
//interface RemoveAdminAction { type: 'REMOVE_ADMIN'; id: string }

type KnownAction = RequestUsersAction | ReceiveUsersAction | AddUserAction | DeleteUserAction | ToggleAdminAction;

export const actionCreators = {
    requestUsers: (): AppThunkAction<KnownAction> => (dispatch, getState) => {
        // Only load data if it's not already loading
        if (getState().users.isLoading === false) {
            let fetchTask = fetch('api/Users/UsersAsync', { credentials: 'same-origin' })
                .then(response => response.json() as Promise<User[]>)
                .then(data => {
                    dispatch({ type: 'RECEIVE_USERS', users: data });
                });

            addTask(fetchTask); // Ensure server-side prerendering waits for this to complete
            dispatch({ type: 'REQUEST_USERS' });
        }
    },
    addUser: (user: User) => <AddUserAction>{ type: 'ADD_USER', user: user },
    deleteUser: (userId: string) => <DeleteUserAction>{ type: 'DELETE_USER', id: userId },
    toggleAdmin: (userId: string) => <ToggleAdminAction>{ type: 'TOGGLE_ADMIN', id: userId },
    //makeAdmin: (userId: string) => <MakeAdminAction>{ type: 'MAKE_ADMIN', id: userId },
    //removeAdmin: (userId: string) => <RemoveAdminAction>{ type: 'REMOVE_ADMIN', id: userId },
};

export const reducer: Reducer<UserState> = (state: UserState, incomingAction: Action) => {
    const action = incomingAction as KnownAction;
    switch (action.type) {
        case 'REQUEST_USERS':
            return { isLoading: true, users: state.users };
        case 'RECEIVE_USERS':
            return { isLoading: false, users: action.users };
        case 'ADD_USER':
            return { isLoading: state.isLoading, users: state.users };
        case 'DELETE_USER':
            return { isLoading: state.isLoading, users: state.users };
        case 'TOGGLE_ADMIN':
            return { isLoading: state.isLoading, users: state.users };
        //case 'MAKE_ADMIN':
        //    return { isLoading: state.isLoading, users: state.users };
        //case 'REMOVE_ADMIN':
        //    return { isLoading: state.isLoading, users: state.users };
        default:
            // The following line guarantees that every action has been covered by a case
            const exhaustiveCheck: never = action;
    }

    // For unrecognized actions (or in cases where actions have no effect), must return the existing state
    //  (or default initial state if none was supplied)
    return state || unloadedState;
};
