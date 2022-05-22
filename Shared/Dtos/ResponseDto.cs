using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wond.Shared.Dtos;

public class ResponseDto {
    public string? Status { get; set; }
    public string? Message { get; set; }
    public object? Data { get; set; }

    public ResponseDto(object? data) {
        Status = "Success";
        Message = "";
        Data = data;
    }

    public ResponseDto(Exception ex) {
        Status = "Failed";
        Message = ex.Message;
        Data = null;
    }

    public ResponseDto(string? status, string? message, object? data) {
        Status = status;
        Message = message;
        Data = data;
    }
}

public class ResponseDto<T> {
    public string? Status { get; set; }
    public string? Message { get; set; }
    public T? Data { get; set; }

    public ResponseDto(T? data) {
        Status = "Success";
        Message = "";
        Data = data;
    }

    public ResponseDto(Exception ex) {
        Status = "Failed";
        Message = ex.Message;
    }

    public ResponseDto(string? status, string? message, T? data) {
        Status = status;
        Message = message;
        Data = data;
    }
}