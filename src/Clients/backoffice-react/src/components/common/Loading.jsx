import React from 'react';
import ReactDelayRender from 'react-delay-render';

const Loading = () => <div class="pageloader"><span class="title">Pageloader</span></div>;

export default ReactDelayRender({ delay: 300 })(Loading);