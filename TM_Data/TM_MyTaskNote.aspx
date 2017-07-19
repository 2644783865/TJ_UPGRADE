<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TM_MyTaskNote.aspx.cs" Inherits="ZCZJ_DPF.TM_Data.TM_MyTaskNote" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>工程备注信息</title>
    <link href="../Assets/main.css" rel="Stylesheet" type="text/css" />
    <base target="_self" />
</head>
<body>
    <form id="form1" runat="server">
            <div class="box-inner">
                <div class="box_right">
                    <div class="box-title">
                        <table width="96%">
                            <tr>
                                <td style="width: 24%">
                                    生产制号:<asp:Label ID="tsaid" runat="server"></asp:Label>
                                </td>
                                <td style="width: 24%">
                                    项目名称:<asp:Label ID="lab_proname" runat="server"></asp:Label>
                                </td>
                                <td style="width: 24%">
                                    设备名称:<asp:Label ID="lab_engname" runat="server"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </div>
         <table width="100%">
          <tr>
           <td align="center">
           <textarea id="TextArea1" runat="server" cols="100" rows="20"></textarea>
           </td>
          </tr>
          <tr>
            <td align="center">
                <asp:button runat="server" id="btnSubmit" onclick="btnSubmit_onclick" onclientclick="return confirm('确认修改吗？\r\r提示:修改将覆盖原记录')" text="提 交" />&nbsp;&nbsp;&nbsp;
                <input id="btnClose" type="button"
                    value="关 闭" onclick="window.close();" />
            </td>
          </tr>
         </table>   
        
    </form>
</body>
</html>
