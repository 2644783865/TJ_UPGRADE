<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Masters/RightCotentMaster.master"
    CodeBehind="OM_SHENHE_Detail.aspx.cs" Inherits="ZCZJ_DPF.OM_Data.OM_SHENHE_Detail" %>

<%--<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>--%>
<asp:Content ID="Content2" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    审核流程配置
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="PrimaryContent" runat="server">

    <script src="../JS/PickPersons.js" type="text/javascript"></script>

    <script type="text/javascript" language="javascript">
//  function CheckInput()
//  {
//      debugger;
//     var arrControls = new Array();
//	 var arrMessages = new Array();
//	
//	 //获取控件ID
//	 arrControls.push("<%=txtleixing.ClientID%>");
//	 arrControls.push("<%=txt_first.ClientID%>");
//	 arrControls.push("<%=txt_second.ClientID%>");
//     arrControls.push("<%=txt_third.ClientID%>");


//     //提示信息
//     arrMessages.push("请填写类型");
//     arrMessages.push("请填写一级");
//     arrMessages.push("请填写二级");
//     arrMessages.push("请填写三级");
//	 if (!InputValidation(arrControls,arrMessages)) 
//	 {
//	     return false;
//	 }
//	 else
//	 {
//	     return true;
//	 }
//  }
  function check()
  {
//  var i;
//  var ss;
//  var dengji=document.getElementById("tdSHJS").getElementsByTagName("input");
//  for(i=0;i<dengji.length;i++)
//  {
//    if(dengji[i].checked)
//    {
//    var aa=dengji[i].value;
//      alert(aa);
//    }
//  }
  
  
var jishu=$("#tdSHJS input:radio:checked").val();
  var leixing=document.getElementById('<%=txtleixing.ClientID %>').value;
  var first=document.getElementById('<%=txt_first.ClientID %>').value;
  var second=document.getElementById('<%=txt_second.ClientID %>').value;
  var third=document.getElementById('<%=txt_third.ClientID %>').value;
  if(leixing=="")
       {
         alert("请首先选择类型");
         return false;
       }
  if(jishu=="1")
  {
     
     if(first=="")
       {
         alert("请填写一级审核人");
          return false;
       }
  }
  if(jishu=="2")
  {
     if(second=="")
       {
         alert("请填写二级审核人");
         return false;
       }
     if(first=="")
       {
         alert("请填写一级审核人");
          return false;
       }
  }
  if(jishu=="3")
  {
     if(third=="")
       {
         alert("请填写三级审核人");
         return false;
       }
        if(second=="")
       {
         alert("请填写二级审核人");
         return false;
       }
     if(first=="")
       {
         alert("请填写一级审核人");
          return false;
       }
  }
  }
  //**********弹出技术部人员子窗口***********************
    var i;
    array=new Array();
    function leixing()
    {
    $("#hidPerson").val("leixing");
    
            SelPersons();
           
//      debugger;
//       i=window.showModalDialog('OM_ShenheREN.aspx?PerInChg=PID','',"dialogHeight:400px;dialogWidth:660px;status:no;scroll:no;center:yes;toolbar=no;menubar=no");
//       if(i!=null)
//     
//       {
//           array=i.split(' ');
//           document.getElementById('<%=txtleixing.ClientID%>').innerText=array[0];
//           document.getElementById('<%=leixingid.ClientID%>').value=array[1];
//       } 
//       else
//       {
//           document.getElementById('<%=txtleixing.ClientID%>').innerText="";
//       }
    }
    function SelTechPersons1()
    {
      $("#hidPerson").val("first");
            SelPersons();
    }
    function SelTechPersons2()
    {
      $("#hidPerson").val("second");
            SelPersons();
    }
    function SelTechPersons3()
    {
    
     $("#hidPerson").val("third");
            SelPersons();
    }
    
    
       //点击确定
        function savePick() {
            var r = Save();
            var id = $("#hidPerson").val();
            if (id == "first") {
                $("#<%=txt_first.ClientID %>").val(r.st_name);
                $("#<%=firstid.ClientID %>").val(r.st_id);
            }
            else if (id == "second") {
                $("#<%=txt_second.ClientID %>").val(r.st_name);
                $("#<%=secondid.ClientID %>").val(r.st_id);
            }
             else if (id == "third") {
                $("#<%=txt_third.ClientID %>").val(r.st_name);
                $("#<%=thirdid.ClientID %>").val(r.st_id);
            }
             else if (id == "leixing") {
         
             var depId=$("#dep").combobox('getValue');
             var deptext=$("#dep").combobox('getText');
             
                $("#<%=txtleixing.ClientID %>").val(deptext);
                $("#<%=leixingid.ClientID %>").val(depId);
            }
          
            $('#win').dialog('close');
        }
    </script>

    <div class="box-wrapper">
        <div class="box-outer">
            <table align="center" width="100%" cellpadding="4" cellspacing="1" class="toptable grid"
                border="1">
                <tr>
                    <td style="font-size: large; text-align: center; height: 43px" colspan="4">
                        审核配置
                    </td>
                </tr>
                <tr>
                    <td class="tdleft1">
                        审核等级
                    </td>
                    <td class="tdright1" id="tdSHJS">
                        <asp:RadioButtonList ID="rblSHJS" runat="server" RepeatColumns="3" RepeatDirection="Horizontal"
                            AutoPostBack="true" OnSelectedIndexChanged="rblSHJS_change">
                            <asp:ListItem Text="一级审核" Value="1"></asp:ListItem>
                            <asp:ListItem Text="二级审核" Value="2"></asp:ListItem>
                            <asp:ListItem Text="三级审核" Value="3" Selected="True"></asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                    <td class="tdleft1">
                        类型
                    </td>
                    <td class="tdright1">
                        <asp:TextBox ID="txtleixing" runat="server" Enabled="false" Width="80px"></asp:TextBox>
                        <font color="#ff0000">*</font>
                        <input id="leixingid" type="text" runat="server" readonly="readonly" style="display: none" />
                        <asp:HyperLink ID="hlleixing" runat="server" CssClass="hand" onClick="leixing()">
                            <asp:Image ID="Image1" ImageUrl="~/Assets/images/h1.gif" border="0" hspace="2" align="absmiddle"
                                runat="server" />
                            选择
                        </asp:HyperLink>
                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="请选择类型"
                            ControlToValidate="txtleixing" Display="Dynamic"></asp:RequiredFieldValidator>--%>
                        <%--                   <font color="#ff0000">*</font>--%>
                    </td>
                </tr>
                <tr>
                    <td class="tdleft1">
                        一级
                    </td>
                    <td class="tdright1">
                        <asp:TextBox ID="txt_first" runat="server" Enabled="false" Width="80px"></asp:TextBox>
                        <font color="#ff0000">*</font>
                        <input id="firstid" type="text" runat="server" readonly="readonly" style="display: none" />
                        <asp:HyperLink ID="hlSelect1" runat="server" CssClass="hand" onClick="SelTechPersons1()">
                            <asp:Image ID="AddImage1" ImageUrl="~/Assets/images/h1.gif" border="0" hspace="2"
                                align="absmiddle" runat="server" />
                            选择
                        </asp:HyperLink>
                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="请选择一级审核人"
                            ControlToValidate="txt_first" Display="Dynamic"></asp:RequiredFieldValidator>--%>
                    </td>
                    <td class="tdleft1">
                        状态
                    </td>
                    <td class="tdright1">
                        <asp:RadioButtonList ID="rblstate" runat="server" RepeatColumns="3" RepeatDirection="Horizontal">
                            <asp:ListItem Text="在用" Value="0" Selected="True"></asp:ListItem>
                            <asp:ListItem Text="停用" Value="1"></asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <tr>
                    <td class="tdleft1">
                        二级
                    </td>
                    <td class="tdright1">
                        <asp:TextBox ID="txt_second" runat="server" Enabled="false" Width="80px"></asp:TextBox>
                        <font color="#ff0000">*</font>
                        <input id="secondid" type="text" runat="server" readonly="readonly" style="display: none" />
                        <asp:HyperLink ID="hlSelect2" runat="server" CssClass="hand" onClick="SelTechPersons2()">
                            <asp:Image ID="AddImage2" ImageUrl="~/Assets/images/h1.gif" border="0" hspace="2"
                                align="absmiddle" runat="server" />
                            选择
                        </asp:HyperLink>
                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="请选择二级审核人"
                            ControlToValidate="txt_second" Display="Dynamic"></asp:RequiredFieldValidator>--%>
                    </td>
                    <td class="tdleft1">
                        三级
                    </td>
                    <td class="tdright1">
                        <asp:TextBox ID="txt_third" runat="server" Enabled="false" Width="80px"></asp:TextBox>
                        <font color="#ff0000">*</font>
                        <input id="thirdid" type="text" runat="server" readonly="readonly" style="display: none" />
                        <asp:HyperLink ID="hlSelect3" runat="server" CssClass="hand" onClick="SelTechPersons3()">
                            <asp:Image ID="AddImage3" ImageUrl="~/Assets/images/h1.gif" border="0" hspace="2"
                                align="absmiddle" runat="server" />
                            选择
                        </asp:HyperLink>
                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="请选择三级审核人"
                            ControlToValidate="txt_third" Display="Dynamic"></asp:RequiredFieldValidator>--%>
                    </td>
                </tr>
                <tr>
                    <td colspan="4" align="center">
                        <asp:Button ID="btnsubmit" runat="server" Text="保 存" OnClick="btnsubmit_Click" OnClientClick="return check();" />
                        &nbsp;&nbsp;
                        <asp:Button ID="btnreturn" runat="server" Text="返 回" OnClick="btnreturn_Click" />
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <div>
        <div id="win" visible="false">
            <div>
                <table>
                    <tr>
                        <td>
                            <strong>指定人员</strong>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        </td>
                        <td>
                            按部门查询：
                        </td>
                        <td>
                            <input id="dep" name="dept" value="02" />
                        </td>
                    </tr>
                </table>
            </div>
            <div style="width: 430px; height: 230px">
                <table id="dg">
                </table>
            </div>
        </div>
        <div id="buttons" style="text-align: right" visible="false">
            <a class="easyui-linkbutton" data-options="iconCls:'icon-ok',plain:true" onclick="savePick();">
                保存</a> <a class="easyui-linkbutton" data-options="iconCls:'icon-cancel',plain:true"
                    onclick="javascript:$('#win').dialog('close')">取消</a>
            <input id="hidPerson" type="hidden" value="" />
        </div>
    </div>
</asp:Content>
