export interface ResponseBase {
    Result: ResultModel;
}

export interface ResultModel {
    IsSuccess: boolean;
    Error: ErrorModel;

}
export interface ErrorModel {
    ErrorText: string;
    ErrorCode: string;
    Exception: string;
}


export interface AddCategoryResponse extends ResponseBase {
    CategoryId: number;
}


export interface DeleteCategoryResponse extends ResponseBase {
    }

    export interface GetCategoryResponse extends ResponseBase {
        CategoryObj: Category;

        Categories: Category[];
        IsResponseTypeList: boolean;
    }

    export interface Category {
        CategoryId: number;
        Name: string;
        TodoList: Todo[];
        IsDeleted?: boolean;
    }

    export interface UpdateCategoryResponse extends ResponseBase {
    }

    export interface AddTodoResponse extends ResponseBase {
        TaskId: number;
    }

    export interface DeleteTodoResponse extends ResponseBase {
    }

    export interface GetTodoResponse extends ResponseBase {
        TodoObj: Todo;
        TodoList: Todo[];

        IsResponseTypeList: boolean;
    }

    export enum TaskStatus {
        Undefined= "Undefined",
        Todo="Todo",
        InProgress= "InProgress",
        Completed="Completed"
    }

    export enum TaskPriority {
        Undefined="Undefined",
        P1="P1",
        P2="P2",
        P3="P3"
    }

    export interface Todo {
        TaskId: number;
        Name: string;
        Status: TaskStatus;
        RootTaskId: number;
        TaskPriority: TaskPriority;
        CreateDate: Date | string;
        IsDeleted?: boolean;
    }

    export interface UpdateTodoResponse extends ResponseBase {
    }

    export interface RegisterUserResponse extends ResponseBase {
    }

    export interface UserLoginResponse extends ResponseBase {
        Token: string;
    }

    export interface AddCategoryDto {
        Category: string;
    }

    export interface DeleteCategoryDto {
        CategoryId: number;
    }

    export interface GetCategoryDto {
        CategoryId: number;
    }

    export interface UpdateCategoryDto {
        CategoryId: number;
        Name: string;
    }

    export interface AddTodoDto {
        Name: string;
        CategoryId: number;
        TaskStatus: TaskStatus;
        RootTaskId?: number;
        TaskPriority: TaskPriority;
    }



    export interface DeleteTodoDto {
        TodoId: number;
    }

    export interface GetTodoDto {
        TaskId: number;
    }

    export interface UpdateTodoDto {
        TaskId: number;
        Name: string;
        CategoryId: number;
        TaskStatus: TaskStatus;
        RootTaskId?: number;
        TaskPriority: TaskPriority;
    }

    export interface User {
        FirstName: string;
        LastName: string;
    }