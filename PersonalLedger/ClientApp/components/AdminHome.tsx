import * as React from 'react';
import { RouteComponentProps } from 'react-router-dom';

export default class AdminHome extends React.Component<RouteComponentProps<{}>, {}> {
    public render() {
        return <div>
            <div className="mainTitle">
                <p className="mainTitle-sub">Chris Carter's</p>
                <p className="mainTitle-main">PersonalLedger</p>
                <p className="mainTitle-sub">Administration</p>
            </div>
        </div>;
    }
}
