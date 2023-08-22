export type CommentItem = {
  id: number;
  avatar: string;
  content: string;
  username: string;
  qq: string;
  github: string;
  githubId: string;
  ip: string;
  link: string;
  status: number;
  post: Post;
  updatedAt: Date;
  createdAt: Date;
};

export type Post = {
  id: number;
  title: string;
};

export type CommentListParams = {
  name?: string;
  status?: string;
  current?: number;
  pageIndex?: number;
};
