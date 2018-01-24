import { fetch, addTask } from 'domain-task';
import { Action, Reducer, ActionCreator } from 'redux';
import { AppThunkAction } from './';

export interface Category {
    id: number;
    name: string;
    tax: boolean;
    type: string;
}

export interface CategoryState {
    isLoading: boolean;
    categories: Category[];
}

const unloadedState: CategoryState = { categories: [], isLoading: false };

interface RequestCategoriesAction { type: 'REQUEST_CATEGORIES' }
interface ReceiveCategoriesAction { type: 'RECEIVE_CATEGORIES'; categories: Category[] }
interface AddCategoryAction { type: 'ADD_CATEGORY'; category: Category }
interface DeleteCategoryAction { type: 'DELETE_CATEGORY'; id: number }
interface UpdateCategoryAction { type: 'UPDATE_CATEGORY'; category: Category }

type KnownAction = RequestCategoriesAction | ReceiveCategoriesAction | AddCategoryAction | DeleteCategoryAction | UpdateCategoryAction;

export const actionCreators = {
    requestCategories: (): AppThunkAction<KnownAction> => (dispatch, getState) => {
        // Only load data if it's not already loading
        if (getState().categories.isLoading === false) {
            let fetchTask = fetch('api/Categories/CategoriesAsync')
                .then(response => response.json() as Promise<Category[]>)
                .then(data => {
                    dispatch({ type: 'RECEIVE_CATEGORIES', categories: data });
                });

            addTask(fetchTask); // Ensure server-side prerendering waits for this to complete
            dispatch({ type: 'REQUEST_CATEGORIES' });
        }
    },
    addCategory: (category: Category) => <AddCategoryAction>{ type: 'ADD_CATEGORY', category: category },
    deleteCategory: (userId: number) => <DeleteCategoryAction>{ type: 'DELETE_CATEGORY', id: userId },
    updateCategory: (category: Category) => <UpdateCategoryAction>{ type: 'UPDATE_CATEGORY', category: category }
};

export const reducer: Reducer<CategoryState> = (state: CategoryState, incomingAction: Action) => {
    const action = incomingAction as KnownAction;
    switch (action.type) {
        case 'REQUEST_CATEGORIES':
            return { isLoading: true, categories: state.categories };
        case 'RECEIVE_CATEGORIES':
            return { isLoading: false, categories: action.categories };
        case 'ADD_CATEGORY':
            return { isLoading: state.isLoading, categories: state.categories };
        case 'DELETE_CATEGORY':
            return { isLoading: state.isLoading, categories: state.categories };
        case 'UPDATE_CATEGORY':
            return { isLoading: state.isLoading, categories: state.categories };
        default:
            // The following line guarantees that every action has been covered by a case
            const exhaustiveCheck: never = action;
    }

    // For unrecognized actions (or in cases where actions have no effect), must return the existing state
    //  (or default initial state if none was supplied)
    return state || unloadedState;
};
