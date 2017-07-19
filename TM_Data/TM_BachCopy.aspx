<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TM_BachCopy.aspx.cs" Inherits="ZCZJ_DPF.TM_Data.TM_BachCopy" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="YYControls" Namespace="YYControls" TagPrefix="yyc" %>
<%@ Register src="../Controls/UCPaging.ascx" tagname="UCPaging" tagprefix="uc1" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<%@ Register src="../Controls/JSRegister.ascx" tagname="JSRegister" tagprefix="JSR" %>
<html xmlns="http://www.w3.org/1999/xhtml" >
<base target="_self" />
<head id="Head1" runat="server">
   <META HTTP-EQUIV="pragma" CONTENT="no-cache"> 
   <META HTTP-EQUIV="Cache-Control" CONTENT="no-cache, must-revalidate"> 
    <META HTTP-EQUIV="expires" CONTENT="Wed, 26 Feb 1997 08:21:57 GMT">
    <title>批量复制数据</title>
     <link href="../Assets/main.css" rel="Stylesheet" type="text/css" />
    <script src="../JS/BOM.js" type="text/javascript" charset="GB2312"></script>
    <script src="../JS/SQL.js" type="text/javascript" charset="GB2312"></script>
    <script src="../JS/TM_BlukCopy.js" type="text/javascript" charset="GB2312"></script>
</head>
<body>
<JSR:JSRegister ID="JSRegister1" runat="server" />
<script language="javascript" type="text/javascript">
   function checkNums(obj)
   {
      var pattem=/^\d+$/;
      var txtnums=obj.value;
      if(!pattem.test(txtnums))
      {
         obj.value="1";
         alert("请输入正确的数量！！！");
      }
   }
   
   function CheckTiaoshu(obj)
   {
      var pattem=/^\d+$/;
      var txtnums=obj.value;
      if(!pattem.test(txtnums))
      {
         obj.value="1";
         obj.focus;
         alert("请输入小于50的正整数！！！");
      }
      else
      {
        if(parseInt(txtnums)>50)
        {
           obj.value="1";
           obj.focus;
           alert("请输入小于50的正整数！！！");
        }
        else if(parseInt(txtnums)<=0)
        {
           obj.value="1";
           obj.focus;
           alert("请输入小于50的正整数！！！");
        }
      }
   }
   //勾选删除
   function checkDelete()
   {
     var tr=document.getElementById("GridView1").getElementsByTagName("tr");
     for(var i=1;i<tr.length;i++)
     {
        obj=tr[i].getElementsByTagName("td")[0].getElementsByTagName("span")[0].getElementsByTagName("input")[0];
        if(obj.checked)
        {    
           document.getElementById("txtDeleteNums").value=i;
           break;
        }
        else
        {
           document.getElementById("txtDeleteNums").value="0";
        }
      }
    }
    //删除确认
    function DeleteConfirm()
    {
       var index=document.getElementById("txtDeleteNums").value;
       if(index=="0")
       {
          alert("请选择要删除的行！！！");
          return false;
       }
       else
       {
          var tt=confirm("确认删除吗？");
          if(tt==true)
          {
             return true;
          }
          else
          {
             return false;
          }
       }
    }
    function CloseWindow()
    {
       var ret=confirm("确认关闭窗口吗？")
       if(ret)
       {
          window.close();
       }
    }
</script>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager> 
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>
    <div class="box-inner">
     <div class="box_right">
       <div class="box-title">
           <table width="98%">
             <tr>
               <td style="width:15%">生产制号：
                <asp:Label ID="tsaid" runat="server"></asp:Label>
               </td>
             <td style="width:20%">项目名称：
                <asp:Label ID="proname" runat="server"></asp:Label>
                <input id="pro_id" type="text" value="" readonly="readonly" runat="server" style="display:none"/>
            </td>
            <td style="width:20%">工程名称：
                <asp:Label ID="engname" runat="server"></asp:Label>
                <input id="eng_type" type="text" value="" readonly="readonly" runat="server" style="display:none"/>
            </td>
            <td align="right">
                <input id="btnClose" type="button" onclick="return CloseWindow();" value="关 闭" /></td>
             </tr>
           </table>
       </div>
     </div>
   </div>
   
    <div class="box-wrapper">
        <div class="box-outer">
        <table width="100%">
        <tr>
         <td align="left">
           <span style="color:Red">&nbsp;&nbsp;&nbsp;鼠标选中复制内容后，按Ctrl+C复制</span>
        </td>
          <td align="left">&nbsp;
             请输入序号:
              <asp:TextBox ID="txtItem" runat="server"></asp:TextBox>&nbsp;&nbsp;
              <asp:Button ID="btnQuery" runat="server" Text="查询" OnClick="btnQuery_OnClick" />&nbsp;&nbsp;
          </td>
        </tr>
        <tr>
         <td colspan="2" align="left">
           &nbsp;&nbsp;&nbsp;待复制数据: 
         </td>
        </tr>
        </table>
      <div id="mySpan" name="mySpan"  contentEditable="true"> 
     <asp:GridView ID="grv" runat="server" AutoGenerateColumns="False" PageSize="20"  CellPadding="4" CssClass="toptable grid" ForeColor="#333333" Width="100%">
      <RowStyle BackColor="#EFF3FB" />
        <Columns>
            <asp:TemplateField HeaderText="行号">
                <ItemTemplate>
                    <asp:Label ID="lblIndex" runat="server" Text="<%# Convert.ToInt32(Container.DataItemIndex +1) %>" />
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" Width="30px" />
            </asp:TemplateField>
            <asp:BoundField DataField="BM_XUHAO" HeaderText="序号" >
                <ItemStyle HorizontalAlign="Center" Wrap="false" />
            </asp:BoundField>
            <asp:BoundField DataField="BM_TUHAO" HeaderText="图号" >
                <ItemStyle HorizontalAlign="Center" Wrap="false" />
            </asp:BoundField>
            <asp:BoundField DataField="BM_ZONGXU" HeaderText="总序" >
                <ItemStyle HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField DataField="BM_MARID" HeaderText="物料编码" >
                <ItemStyle HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField DataField="BM_CHANAME" HeaderText="名称" >
                <ItemStyle HorizontalAlign="Center" Wrap="false" />
            </asp:BoundField>
            <asp:BoundField DataField="BM_GUIGE" HeaderText="规格" >
                <ItemStyle HorizontalAlign="Center" Wrap="false" />
            </asp:BoundField>
            <asp:BoundField DataField="BM_MALENGTH" HeaderText="材料长度" >
                <ItemStyle HorizontalAlign="Center" Wrap="false" />
            </asp:BoundField>
            <asp:BoundField DataField="BM_MAWIDTH" HeaderText="材料宽度" >
                <ItemStyle HorizontalAlign="Center" Wrap="false" />
            </asp:BoundField>
            <asp:BoundField DataField="BM_NUMBER" HeaderText="数量" >
                <ItemStyle HorizontalAlign="Center" Wrap="false" />
            </asp:BoundField>
            <asp:BoundField DataField="BM_MAUNITWGHT" HeaderText="材料单重" >
                <ItemStyle HorizontalAlign="Center" Wrap="false" />
            </asp:BoundField>
            <asp:BoundField DataField="BM_MATOTALWGHT" HeaderText="材料总重" >
                <ItemStyle HorizontalAlign="Center" Wrap="false" />
            </asp:BoundField>
            <asp:BoundField DataField="BM_UNITWGHT" HeaderText="单重" >
                <ItemStyle HorizontalAlign="Center" Wrap="false" />
            </asp:BoundField>
            <asp:BoundField DataField="BM_TOTALWGHT" HeaderText="总重" >
                <ItemStyle HorizontalAlign="Center" Wrap="false" />
            </asp:BoundField>
             <asp:BoundField DataField="BM_MATOTALLGTH" HeaderText="材料总长" >
                <ItemStyle HorizontalAlign="Center" Wrap="false" />
            </asp:BoundField>
             <asp:BoundField DataField="BM_FIXEDSIZE" HeaderText="是否定尺" >
                <ItemStyle HorizontalAlign="Center" Wrap="false" />
            </asp:BoundField>    
            <asp:BoundField DataField="BM_MAQUALITY" HeaderText="材质" >
                <ItemStyle HorizontalAlign="Center" Wrap="false" />
            </asp:BoundField>       
        </Columns>
            <PagerStyle CssClass="bomcolor" ForeColor="#EEF7FD" HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            <HeaderStyle BackColor="#B9D3EE" Font-Bold="True"  ForeColor="#1E5C95" />
            <EditRowStyle BackColor="#2461BF" />
            <AlternatingRowStyle BackColor="White" />
        </asp:GridView>
      <asp:Panel ID="NoDataPanel" runat="server" HorizontalAlign="Center"><hr style="width:100%; height:0.1px; color:Blue;" />没有记录!</asp:Panel>
      </div>
       <br />
       <hr style="width:100%; height:0.1px; color:Blue;" />
      <table width="100%">
       <tr>
        <td>&nbsp;&nbsp;&nbsp;复制增加数据:</td>
         <td align="right">起始总序:<asp:TextBox ID="txtZongXu" Width="100px" runat="server"></asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;
           复制条数:<asp:TextBox ID="txtNum" Text="1" Width="50px" ToolTip="不超过50行" onblur="CheckTiaoshu(this);" runat="server"></asp:TextBox>&nbsp;&nbsp;
             <asp:Button ID="btnAdd" runat="server" Visible="false" Text="复制" OnClick="btnAdd_OnClick" />&nbsp;&nbsp;
             <asp:Button ID="btnDelete" runat="server" Visible="false" OnClientClick="DeleteConfirm();" Text="删除" OnClick="btnDelete_OnClick" />&nbsp;&nbsp;
             <asp:Button ID="btnSave" runat="server" Visible="false" Text="保存" OnClientClick="return confirm('确认保存吗？');" OnClick="btnSave_OnClick" />&nbsp;&nbsp;
             <input id="txtDeleteNums" runat="server" value="0" type="text" readonly="readonly" style="display:none" />
           </td>
         <td></td>
         <td></td>
       </tr>
      </table>
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" PageSize="20" 
            CellPadding="4" CssClass="toptable grid" ForeColor="#333333" Width="100%">
            <RowStyle BackColor="#EFF3FB" />
            <Columns>
                <asp:TemplateField ItemStyle-Width="10px">
                    <ItemTemplate>
                        <asp:CheckBox ID="CheckBox1" runat="server" Width="10px" onclick="return checkDelete()" CssClass="checkBoxCss"/>
                    </ItemTemplate>
                    <ItemStyle Width="10px" />
                </asp:TemplateField>
                
                <asp:TemplateField HeaderText="序号" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Label ID="Label1" runat="server" Text='<%# Convert.ToInt32(Container.DataItemIndex +1) %>'></asp:Label>                    </ItemTemplate>
                </asp:TemplateField>
                
                <asp:TemplateField HeaderText="图号" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <input ID="tuhao" runat="server" onkeydown="grControlFocusWithoutHiddden(this);" style="border-style:none;" type="text" 
                            value='<%#Eval("BM_TUHAO") %>' />
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                
                <asp:TemplateField HeaderText="总序" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <input ID="zongxu" runat="server" onkeydown="grControlFocusWithoutHiddden(this);" onblur="Batchverify(this)" onclick="BatchAutoXuhao(this);" style="border-style:none; width:90px" type="text" 
                            value='<%#Eval("BM_ZONGXU") %>' />
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                
                <asp:TemplateField HeaderText="中文名称">
                    <ItemTemplate>
                        <input ID="ch_name" runat="server" onkeydown="grControlFocusWithoutHiddden(this);" style="border-style:none;" 
                            type="text" value='<%#Eval("BM_CHANAME") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                
                <asp:TemplateField HeaderText="数量" ItemStyle-BackColor="#FFFFCC">
                    <ItemTemplate>
                        <input ID="shuliang" runat="server" onkeydown="grControlFocusWithoutHiddden(this);" onblur="checkNums(this)" 
                            style="border-style:none; width:36px" type="text" 
                            value='<%#Eval("BM_NUMBER") %>' />
                    </ItemTemplate>
                    <ItemStyle BackColor="#FFFFCC" />
                </asp:TemplateField>
                
                <asp:BoundField HeaderText="材质" ItemStyle-HorizontalAlign="Center" DataField="BM_MAQUALITY" />
                <asp:TemplateField HeaderText="备注">
                    <ItemTemplate>
                        <input ID="beizhu" runat="server" onkeydown="grControlFocusWithoutHiddden(this);" style="border-style:none;" 
                            type="text" value='<%#Eval("BM_NOTE") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
              <asp:TemplateField HeaderText="是否定尺">
                <ItemTemplate>
                  <asp:DropDownList ID="ddlFixedSize" runat="server" DataValueField='<%#DataBinder.Eval(Container.DataItem, "BM_FIXEDSIZE") %>'>
                    <asp:ListItem Text="-- --" Value="N" Selected="True"></asp:ListItem>
                    <asp:ListItem Text="定尺" Value="Y"></asp:ListItem>
                  </asp:DropDownList>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" Width="90px" />
            </asp:TemplateField>
            </Columns>
            <PagerStyle CssClass="bomcolor" ForeColor="#EEF7FD" HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            <HeaderStyle BackColor="#B9D3EE" Font-Bold="True"  ForeColor="#1E5C95" />
            <EditRowStyle BackColor="#2461BF" />
            <AlternatingRowStyle BackColor="White" />
        </asp:GridView>
    </div>
    </div>
    </ContentTemplate>
</asp:UpdatePanel>
<asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1" DisplayAfter="1">
    <ProgressTemplate>
       <div style="position: absolute; top: 10%; right:40%">
       <table>
       <tr>
       <td align="right"><asp:Image ID="Image1" runat="server" ImageUrl="~/Assets/images/ajax-loader2.gif" /></td>
       <td align="left" style="background-color:Yellow; font-size:medium;">数据处理中，请稍后...</td>
       </tr>
       </table>
       </div>
    </ProgressTemplate>
</asp:UpdateProgress>
    </form>
</body>
</html>
