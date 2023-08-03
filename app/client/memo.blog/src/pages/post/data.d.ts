export interface CommentType {
  id: number;
  isAutor: boolean;
  avatar?: string;
  name?: string;
  date?: string;
  content?: string;
  sort: number;
}
