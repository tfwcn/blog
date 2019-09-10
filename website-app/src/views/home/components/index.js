import React from 'react';
import LayoutMain from '../../shared/components/layoutMain';
import News from './news';
import style from '../styles/index.module.scss';

const HomeIndex = () => {
    document.title = 'TFW';
    return (
        <LayoutMain>
            <div className={style.item}>
                <News />
            </div>
        </LayoutMain>
    );
}

export default HomeIndex;