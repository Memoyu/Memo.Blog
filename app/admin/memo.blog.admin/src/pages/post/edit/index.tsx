import { MdEditor } from 'md-editor-rt';
import 'md-editor-rt/lib/style.css';
import React, { useState } from 'react';
import { useRequest } from 'umi';
import { PostDetail } from './data';
import { postDetail } from './service';

export const PostEdit: React.FC = () => {
  const { data } = useRequest<{ data: PostDetail }>(postDetail);
  const [content, setContent] = useState(data?.content || '');

  return (
    <div>
      <MdEditor modelValue={content} onChange={setContent} />
    </div>
  );
};

export default PostEdit;
