import React from 'react';
import { useState, useEffect } from 'react';
import { Button, Toast } from '@douyinfe/semi-ui';

const Home = () => {
  return (
    <div className="home">
      <h1>Home</h1>
      <Button onClick={() => Toast.warning({ content: 'welcome' })}>Hello Semi</Button>
    </div>
  );
};

export default Home;
