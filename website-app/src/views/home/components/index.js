import React from 'react';
import News from './news'
import style from '../styles/index.module.scss'

const HomeIndex = () => {
    return (
        <div className={style.body}>
            <div className={style.header}>
                <span className={style.title}>TFW</span>
            </div>
            <div className={style.item}>
                <News/>
            </div>
        </div>
    );
}

export default HomeIndex;