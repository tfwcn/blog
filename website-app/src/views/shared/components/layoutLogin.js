import React from 'react';
import { Link } from "react-router-dom";
import PropTypes from 'prop-types';
import style from '../styles/layoutMain.module.scss';

class LayoutLogin extends React.Component {
    // 渲染
    render() {
        return (
            <div className={style.body}>
                <div className={style.header}>
                    <Link className={style.title} to='/'>TFW</Link>
                </div>
                {this.props.children}
            </div>
        );
    }
}
LayoutLogin.propTypes = {
    children: PropTypes.element,
};

export default LayoutLogin;