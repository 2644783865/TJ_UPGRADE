<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TM_MSAdjustInput.aspx.cs" Inherits="ZCZJ_DPF.TM_Data.TM_MSAdjustInput" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
     <base target="_self" />
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <title>添加组装件</title>
    <link href="../Assets/main.css" rel="Stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript">
       function MSXuHaoCheck(obj)
       {
          var checktext=obj.value.replace(/(\s*$)/g, "");
          var pattem2=/^[1-9][0-9]*$/;
          if(checktext!="")
          {
              if(!pattem2.test(checktext))
              {
                 alert("数量输入格式不正确！！！");
                 obj.value="1";
              }
          }
          else
          {
             obj.value="1";
          }
       }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="box-inner" style="height:40px;">
         <div class="box_right">
             <div class="box-title">
                 <table width="100%">
                    <tr>
                <td style="width:40%">生产制号：
                <asp:Label ID="tsaid" runat="server"></asp:Label>
               </td>
            <td style="width:30%">项目名称：
                <asp:Label ID="proname" runat="server"></asp:Label>
                <input id="pro_id" type="text" value="" readonly="readonly" runat="server" style="display:none"/>
            </td>
           </tr>
           <tr><td style="width:30%">工程名称：
                <asp:Label ID="engname" runat="server"></asp:Label>
                <input id="eng_type" type="text" value="" readonly="readonly" runat="server" style="display:none"/>
            </td>
            <td>台数:<asp:Label ID="lblNumber" runat="server" Text=""></asp:Label></td>
                    </tr>
                 </table>
             </div>
         </div>
     </div>
         <div class="box-wrapper">
        <div class="box-outer">
           <table width="100%" align="center" cellpadding="4" cellspacing="1" class="toptable grid" border="1">
               <tr>
                  <td  align="right"><strong>明细序号:</strong></td>
                  <td colspan="3"><asp:TextBox ID="txtMs" runat="server"></asp:TextBox></td>
               </tr>
               <tr>
                 <td align="right"><strong>序号:</strong></td>
                 <td><asp:TextBox ID="txtXuHao" ToolTip="必填项" runat="server"></asp:TextBox></td>
                 <td align="right"><strong>总序:</strong></td>
                 <td><asp:TextBox ID="txtZongxu" ToolTip="可不填" runat="server" AutoPostBack="true" OnTextChanged="txtZongxu_OnTextChanged"></asp:TextBox></td>
               </tr>
               <tr>
                 <td align="right"><strong>图号(标识号):</strong></td>
                 <td ><asp:TextBox ID="txtTuHao" ToolTip="可不填" runat="server"></asp:TextBox></td>
                 <td align="right"><strong>单台数量:</strong></td>
                 <td><asp:TextBox ID="txtMSXuHao" Text="1" onblur="MSXuHaoCheck(this);" ToolTip="必填项" runat="server"></asp:TextBox></td>
               </tr>
               <tr>
               <td align="right"><strong>名称:</strong></td>
               <td><asp:TextBox ID="txtMC" ToolTip="必填项" runat="server"></asp:TextBox></td>
               <td align="right"><strong>库:</strong></td>
               <td><asp:TextBox ID="txtKu" ToolTip="可不填" Text="库" runat="server"></asp:TextBox></td>
               </tr>
               <tr>
               <td align="right"><strong>备注:</strong></td>
               <td colspan="3"><asp:TextBox ID="txtBZ" ToolTip="可不填" TextMode="MultiLine" Width="70%" Height="30px" runat="server"></asp:TextBox></td>
               </tr>
               <tr>
               <td colspan="4" align="center">
                   <asp:Button ID="btnSave" runat="server" Text="保存" OnClick="btnSave_OnClick" OnClientClick="return confirm('确认提交吗？');" />&nbsp;&nbsp;&nbsp;&nbsp;
                   <input id="btnCancel" type="button" value="关闭" title="不保存数据,直接关闭窗口" onclick="window.close();" />
                   
               </td>
               </tr>
           </table>
        </div>
        </div>
    </form>
</body>
</html>
