import React, { useState } from 'react';
import { Tooltip, Input, TextArea, Button } from '@douyinfe/semi-ui';
import { IconEmoji, IconBrackets, IconSend } from '@douyinfe/semi-icons';
import classNames from 'classnames';
//import defaultAvatar from '@/assets/images/avatar/default.png';
import s from './index.module.scss';

const CommentEdit: React.FC = () => {
  const [avatar, setAvatar] = useState(require('@/assets/images/avatar/default.png'));
  const [avatarSelectVisible, setAvatarSelectVisible] = useState(false);

  const onQqInputEnterPress = (e: React.KeyboardEvent<HTMLInputElement>) => {
    console.log(e.target.value);
  };

  return (
    <div>
      <div className={s.commentEdit}>
        <div className={s.commentUser}>
          <div className={s.avatarBox}>
            <Tooltip
              style={{ backgroundColor: 'white' }}
              trigger="click"
              visible={avatarSelectVisible}
              content={
                <div>
                  <Input prefix="QQ头像：" showClear onEnterPress={(e) => onQqInputEnterPress(e)}></Input>
                </div>
              }>
              <img className={s.avatar} src={avatar} />
            </Tooltip>
          </div>
          <Input className={s.inputInfo} placeholder={'昵称'}></Input>
          <Input className={s.inputInfo} placeholder={'邮箱(选填)'}></Input>
        </div>
        <TextArea className={s.commentTextArea} maxCount={100} showClear />
        <div className={s.commentBtns}>
          <div className={s.functionBtns}>
            <Button icon={<IconEmoji />} aria-label="emoji" />
          </div>
          <div className={s.operateBtns}>
            <Button icon={<IconBrackets />} className={s.itemBtn} aria-label="emoji">
              预览
            </Button>
            <Button icon={<IconSend />} className={s.itemBtn} aria-label="emoji">
              发送
            </Button>
          </div>
        </div>
      </div>
    </div>
  );
};

export default CommentEdit;
