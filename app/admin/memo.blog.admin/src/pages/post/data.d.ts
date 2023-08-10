export type PostItem = {
  id: number;
  title: string;
  category: Category;
  tags: Array<PostTag>;
  status: number;
  updatedAt: Date;
  createdAt: Date;
};

export type TableListPagination = {
  total: number;
  pageSize: number;
  current: number;
};

export type TableListData = {
  list: PostItem[];
  pagination: Partial<TableListPagination>;
};

export type Category = {
  id: number;
  Name: string;
};

export type PostTag = {
  id: number;
  Name: string;
};

export type TableListParams = {
  status?: string;
  name?: string;
  desc?: string;
  key?: number;
  pageSize?: number;
  currentPage?: number;
  filter?: Record<string, any[]>;
  sorter?: Record<string, any>;
};
