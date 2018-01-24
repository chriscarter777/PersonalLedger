import * as React from 'react';
import { Link, RouteComponentProps } from 'react-router-dom';
import { connect } from 'react-redux';
import { ApplicationState } from '../store';
import * as CategoryStore from '../store/Categories';

type AdminCategoriesProps =
    CategoryStore.CategoryState
    & typeof CategoryStore.actionCreators
    & RouteComponentProps<{}>;

export class AdminCategories extends React.Component<AdminCategoriesProps, {}> {
    componentWillMount() {
        // This method runs when the component is first added to the page
        this.props.requestCategories();
    }

    componentWillReceiveProps(nextProps: AdminCategoriesProps) {
        // This method runs when incoming props (e.g., route params) change
        this.props.requestCategories();
    }


    public render() {
        var greenStyle = { color: 'green' };
        var categoryItems = this.props.categories.map(function (item) {
            return (
                <li>
                    {item.id} : {item.name} : {item.tax && <span style={greenStyle}>Tax</span>} : {item.type}
                </li>
            );
        });

        return <div>
            <h1 className="captionlike">Admin-Categories</h1>

            <ul>
                {categoryItems}
            </ul>

            <button onClick={() => { this.props.addCategory(this.props.categories[0]) }}>Add</button>
            <button onClick={() => { this.props.deleteCategory(0) }}>Delete</button>
            <button onClick={() => { this.props.updateCategory(this.props.categories[0]) }}>Edit</button>
        </div>;
    }
}

// Wire up the React component to the Redux store
// Selects which state properties are merged into the component's props
export default connect((state: ApplicationState) => state.categories, CategoryStore.actionCreators)(AdminCategories) as typeof AdminCategories;