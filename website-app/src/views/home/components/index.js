import React from 'react';
import { Link } from "react-router-dom";
import News from './news';
import style from '../styles/index.module.scss';

const HomeIndex = () => {
    document.title = 'TFW';
    return (
        <div className={style.body}>
            <div className={style.header}>
                <Link className={style.title} to='/'>TFW</Link>
                <Link className={style.login} to='/login'>登录</Link>
            </div>
            <div className={style.item}>
                <News/>
            </div>
        </div>
    );
}

export default HomeIndex;