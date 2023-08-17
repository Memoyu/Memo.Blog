import { MdEditor } from 'md-editor-rt';
import 'md-editor-rt/lib/style.css';
import React, { useState } from 'react';
import { useRequest } from 'umi';
import { PostDetail } from './data';
import { postDetail } from './service';

export const PostEdit: React.FC = () => {
  const [content, setContent] = useState('');
  const [toolbars] = useState([
    'bold',
    'underline',
    'italic',
    '-',
    'strikeThrough',
    'sub',
    'sup',
    'quote',
    'unorderedList',
    'orderedList',
    'task',
    '-',
    'codeRow',
    'code',
    'link',
    'image',
    'table',
    'mermaid',
    'katex',
    '-',
    'revoke',
    'next',
    'save',
    '=',
    'pageFullscreen',
    'fullscreen',
    'preview',
    'htmlPreview',
    'catalog',
  ]);
  useRequest<{ data: PostDetail }>(postDetail, {
    onSuccess: (data) => {
      setContent(data.content);
    },
  });

  return (
    <div>
      <MdEditor modelValue={content} toolbars={toolbars} onChange={setContent} />
    </div>
  );
};

export default PostEdit;
