import * as React from 'react';
import { RouteComponentProps } from 'react-router-dom';

export default class MainHome extends React.Component<RouteComponentProps<{}>, {}> {
    public render() {
        return <div>
            <div className="mainTitle">
                <p className="mainTitle-sub">Chris Carter's</p>
                <p className="mainTitle-main">PersonalLedger</p>
            </div>
            <div className="center lime">
                <p>A home bookkeeping application, using traditional debit-credit methodology, built as a hybrid MVC/SPA using:</p>
                <ul className="unstyledList left indented35">
                    <li><a href='https://get.asp.net/'>ASP.NET Core</a> and <a href='https://msdn.microsoft.com/en-us/library/67ef8sbd.aspx'>C#</a> for cross-platform server-side code</li>
                    <li><a>IdentityCore</a> for authentication and security</li>
                    <li><a>EntityFrameworkCore</a> for relational object mapping</li>
                    <li><a>XUnit for testing</a></li>
                    <li><a href='https://facebook.github.io/react/'>React</a>, <a href='http://redux.js.org'>Redux</a>, and <a href='http://www.typescriptlang.org/'>TypeScript</a> for client-side code</li>
                    <li><a href='https://webpack.github.io/'>Webpack</a> for building and bundling client-side resources</li>
                    <li><a href='http://getbootstrap.com/'>Bootstrap</a> for layout and styling</li>
                </ul>
            </div>
        </div>;
    }
}
