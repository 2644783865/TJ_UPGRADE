<%@ Page Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master" AutoEventWireup="true"
    CodeBehind="QC_Inspection_Add_Blank.aspx.cs" Inherits="ZCZJ_DPF.QC_Data.QC_Inspection_Add_Blank"
    Title="无标题页" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">

    <script src="javascripts/QCControl.js" type="text/javascript"></script>

    <script type="text/javascript" language="javascript">
    function GetPjandEng(obj)
    {
        var text=obj.value;
        var n;
        if(text!="") {
            if (text.indexOf('|')>-1) {
            obj.style.background="white";
           
            var n=text.lastIndexOf('|');
            var m=text.indexOf('|');
           
            var eng=text.substring(n+1);
            var pj=text.substring(m+1,n);
            var engid=text.substring(0,m);
          
                var obj_pj=document.getElementById("<%=TextBoxProName.ClientID %>");
                var obj_eng=document.getElementById("<%=TextBoxEngNm.ClientID %>");
                obj_pj.value=pj;
                obj_eng.value=eng;
                obj.value=engid;
            } 
        }
    }
    function CheckNews()
    {
        var arrControls = new Array();
	    var arrMessages = new Array();
    	
	    //获取控件ID
	    arrControls.push("<%=TextBoxEngID.ClientID%>");
	    arrControls.push("<%=TextBoxProName.ClientID%>");
	    arrControls.push("<%=TextBoxEngNm.ClientID%>");
	    arrControls.push("<%=TextBoxPartName.ClientID%>");
	    arrControls.push("<%=TextBoxSupplier.ClientID%>");
        arrControls.push("<%=TextBoxSite.ClientID%>");
        arrControls.push("<%=TextBoxContracter.ClientID %>");
        arrControls.push("<%=TextBoxTel.ClientID %>");

        //提示信息
        arrMessages.push("请填写生产制号！");
        arrMessages.push("请正确填写生产制号！");
        arrMessages.push("请正确填写生产制号！");
        arrMessages.push("请填写部件名称！");
        arrMessages.push("请填写供货单位！");
        arrMessages.push("请填写检查地点！");
        arrMessages.push("请填写联系人！");
        arrMessages.push("请填写联系电话！");
	    if (!CheckInput(arrControls,arrMessages)) 
	    {
	        return false;
	    }
	    else
	    {
	        return true;
	    }
    }

var hintColor="yellow"; //警告色
function CheckInput(arrField,arrRemind)
{
	len1=arrField.length; 
	len2=arrRemind.length; 

	if(len1!=len2) {
		alert("参数错误！");
		
		return false;
	}
	//输入非空验证
	var n=0;
	for (k=0;k<len1;k++)
	{	
		_tmpObj=document.all(arrField[k]);
			if(_tmpObj.value.trim()=="")
			{
			     n++;
				_tmpObj.style.background=hintColor;
				alert(arrRemind[k]);
				return false;
			}
			else
			{
    			_tmpObj.style.background="white";
			}
      }
      if(n==0)
       {
           return true;
       }
}
    function CheckNum(obj)
    {
        if(isNaN(parseFloat(obj.value)))
        {
           alert("请输入正确的数量!");
           obj.value=0;
           return; 
        }
        CheckSumW(obj);
    } 
     function CheckWeight(obj)
    {
         if(isNaN(parseFloat(obj.value)))
        {
           alert("请输入正确的单重!");
           obj.value=0;
           return;
        }
        CheckSumW(obj);
    }
    function CheckSumW(obj)
    {
        var tr=obj.parentNode.parentNode;
        var obj_num=tr.getElementsByTagName("td")[3].getElementsByTagName("input")[0];
        var obj_dz=tr.getElementsByTagName("td")[4].getElementsByTagName("input")[0];
        if(obj_num.value!=""&&obj_dz.value!="")
        {  
             tr.getElementsByTagName("td")[5].getElementsByTagName("input")[0].value=(obj_num.value)*(obj_dz.value);
        }
    }
    
      
    </script>

    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnableScriptGlobalization="true"
        EnableScriptLocalization="true">
    </asp:ToolkitScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="box-inner">
                <div class="box_right">
                    <div class="box-title">
                        <table width="100%">
                            <tr>
                                <td align="right">
                                    <asp:Button ID="Submit" runat="server" Text="保存" OnClientClick="return CheckNews()"
                                        OnClick="Submit_Click" />&nbsp; &nbsp;
                                    <input id="Cancel" type="button" value="取消" onclick="return history.go(-1)" />&nbsp;
                                    &nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </div>
            <div class="box-wrapper">
                <div class="box-outer">
                    <table cellpadding="4">
                        <tr>
                            <td colspan="2">
                            </td>
                            <td colspan="3">
                                <div align="center">
                                    <h2>
                                        <strong>报检通知单</strong></h2>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" colspan="2">
                                任务号:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:TextBox ID="TextBoxEngID" runat="server" onblur="GetPjandEng(this)"></asp:TextBox>
                                <asp:AutoCompleteExtender ID="AceEngID" runat="server" TargetControlID="TextBoxEngID"
                                    CompletionSetCount="10" DelimiterCharacters="" Enabled="True" MinimumPrefixLength="1"
                                    ServiceMethod="getQCEngID" FirstRowSelected="true" ServicePath="~/Ajax.asmx"
                                    UseContextKey="True">
                                </asp:AutoCompleteExtender>
                                <asp:Label ID="LabelState" runat="server" Text="" Visible="false"></asp:Label>
                                <asp:TextBox ID="TextBoxData" runat="server" Visible="false"></asp:TextBox>
                            </td>
                            <td width="80" align="center">
                                项目名称:
                            </td>
                            <td width="150" align="left">
                                <input id="TextBoxProName" runat="server" type="text" />
                            </td>
                            <td width="80" align="center">
                                设备名称:
                            </td>
                            <td width="150" align="left">
                                <input id="TextBoxEngNm" runat="server" type="text" />
                            </td>
                            <td width="80" align="center">
                                报检部门:
                            </td>
                            <td width="150" align="left">
                                <asp:TextBox ID="TextBoxDep" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="center">
                                部件名称:
                            </td>
                            <td align="left">
                                <asp:TextBox ID="TextBoxPartName" runat="server"></asp:TextBox>
                                <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="*" ControlToValidate="TextBoxPartName" Display="Dynamic"></asp:RequiredFieldValidator>--%>
                            </td>
                            <td align="center">
                                供货单位:
                            </td>
                            <td align="left">
                                <asp:TextBox ID="TextBoxSupplier" runat="server" AutoPostBack="True" OnTextChanged="Fillcontract"></asp:TextBox>
                            </td>
                            <td align="center">
                                检查地点:
                            </td>
                            <td align="left">
                                <asp:TextBox ID="TextBoxSite" runat="server"></asp:TextBox>
                            </td>
                            <td width="80" align="center">
                                报检人:
                            </td>
                            <td width="150" align="left">
                                <asp:TextBox ID="TextBoxMan" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="center">
                                联系人:
                            </td>
                            <td>
                                <asp:TextBox ID="TextBoxContracter" runat="server">
       
                                </asp:TextBox>
                            </td>
                            <td align="center">
                                联系电话:
                            </td>
                            <td>
                                <asp:TextBox ID="TextBoxTel" runat="server"></asp:TextBox>
                            </td>
                            <td align="center">
                                需要质检时间:
                            </td>
                            <td>
                                <asp:TextBox ID="TextBoxDate" runat="server"></asp:TextBox>
                                <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="TextBoxDate"
                                    TodaysDateFormat="yyyy-MM-dd" Format="yyyy-MM-dd" FirstDayOfWeek="Monday">
                                </asp:CalendarExtender>
                            </td>
                            <td align="center">
                                备注:
                            </td>
                            <td>
                                <asp:TextBox ID="TextBoxNote" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" colspan="8">
                                <asp:Button ID="Insert" runat="server" Text="插入" OnClick="Insert_Click" />&nbsp;&nbsp;
                                <asp:Button ID="Delete" runat="server" Text="删除" OnClick="Delete_Click" />
                            </td>
                        </tr>
                    </table>
                    <table cellpadding="4" cellspacing="1" class="toptable grid" border="1">
                        <asp:Repeater ID="RepeaterItem" runat="server">
                            <HeaderTemplate>
                                <tr class="tableTitle1">
                                    <td width="36" rowspan="2">
                                        <div align="center">
                                            序号</div>
                                    </td>
                                    <td width="80" rowspan="2">
                                        <div align="center">
                                            子项名称</div>
                                    </td>
                                    <td width="100" rowspan="2">
                                        <div align="center">
                                            图号/标识符</div>
                                    </td>
                                    <td width="50" rowspan="2">
                                        <div align="center">
                                            报检数量</div>
                                    </td>
                                    <td colspan="2">
                                        <div align="center">
                                            重量(kg)</div>
                                    </td>
                                    <td width="80" rowspan="2">
                                        <div align="center">
                                            交货状态</div>
                                    </td>
                                    <td width="90" rowspan="2">
                                        <div align="center">
                                            检查内容</div>
                                    </td>
                                </tr>
                                <tr class="tableTitle1">
                                    <td width="50">
                                        <div align="center">
                                            单重</div>
                                    </td>
                                    <td width="50">
                                        <div align="center">
                                            总重</div>
                                    </td>
                                </tr>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr align="center" class="baseGadget" onmousemove="this.className='highlight'" onmouseout="this.className='baseGadget'">
                                    <td width="36">
                                        <asp:CheckBox ID="CheckBox1" runat="server" /><%#Container.ItemIndex+1%>
                                        <asp:Label ID="LabelKey" runat="server" Visible="false"></asp:Label>
                                    </td>
                                    <td width="80">
                                        <asp:TextBox ID="TextBoxMarName" runat="server" Text='<%#Eval("PARTNM") %>' onkeyup="grControlFocus(this)"></asp:TextBox>
                                    </td>
                                    <td width="100">
                                        <asp:TextBox ID="TextBoxDrawingNO" runat="server" Text='<%#Eval("TUHAO") %>' onkeyup="grControlFocus(this)"></asp:TextBox>
                                    </td>
                                    <td width="50">
                                        <asp:TextBox ID="TextBoxNum" runat="server" Text='<%#Eval("PJNUM") %>' onkeyup="grControlFocus(this)"
                                            onblur="CheckNum(this)"></asp:TextBox>
                                    </td>
                                    <td width="50">
                                        <asp:TextBox ID="TextBoxSW" runat="server" Text='<%#Eval("DANZHONG") %>' onkeyup="grControlFocus(this)"
                                            onblur="CheckWeight(this)"></asp:TextBox>
                                    </td>
                                    <td width="50">
                                        <asp:TextBox ID="TextBoxSumW" runat="server" Text='<%#Eval("ZONGZHONG") %>' onkeyup="grControlFocus(this)"></asp:TextBox>
                                    </td>
                                    <td width="80">
                                        <asp:TextBox ID="TextBoxState" runat="server" Text='<%#Eval("JHSTATE") %>' onkeyup="grControlFocus(this)"
                                            AutoPostBack="true" OnTextChanged="Filljhstate"></asp:TextBox>
                                    </td>
                                    <td width="80">
                                        <asp:TextBox ID="TextBoxControlContent" runat="server" Text='<%#Eval("CONT") %>'
                                            onkeyup="grControlFocus(this)" AutoPostBack="true" OnTextChanged="Fillcontent"></asp:TextBox>
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </table>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
