import * as React from 'react';
import { Link, RouteComponentProps } from 'react-router-dom';
import { connect } from 'react-redux';
import { ApplicationState } from '../store';
import * as CategoryStore from '../store/Categories';

type CategoriesProps =
    CategoryStore.CategoryState
    & typeof CategoryStore.actionCreators
    & RouteComponentProps<{}>;

export class Categories extends React.Component<CategoriesProps, {}> {
    componentWillMount() {
        // This method runs when the component is first added to the page
        this.props.requestCategories();
    }

    //componentWillReceiveProps(nextProps: CategoriesProps) {
    //    // This method runs when incoming props (e.g., route params) change
    //    this.props.requestCategories();
    //}

    public render() {
        var greenStyle = { color: 'green' };
        var categoryItems = this.props.categories.map((item) =>
            <tr key={item.id.toString()}>
                <td>{item.id}</td>
                <td>{item.name}</td>
                <td>&nbsp;{item.tax && <span className='glyphicon glyphicon-copy' style={greenStyle}></span>}</td>
                <td>{item.type}</td>
            </tr>
        );

        return <div>
            <table>
                <caption>Categories</caption>
                <thead>
                    <tr>
                        <th>ID</th>
                        <th>Name</th>
                        <th>Tax?</th>
                        <th>Type</th>
                    </tr>
                </thead>
                <tbody>
                    {categoryItems}
                </tbody>
            </table>
            <button onClick={() => { this.props.addCategory(this.props.categories[0]) }}>Add</button>
            <button onClick={() => { confirm('are you sure you want to delete this category?'); this.props.deleteCategory(0) }}>Delete</button>
            <button onClick={() => { this.props.updateCategory(this.props.categories[0]) }}>Edit</button>
        </div>;
    }
}

// Wire up the React component to the Redux store
// Selects which state properties are merged into the component's props
export default connect((state: ApplicationState) => state.categories, CategoryStore.actionCreators)(Categories) as typeof Categories;