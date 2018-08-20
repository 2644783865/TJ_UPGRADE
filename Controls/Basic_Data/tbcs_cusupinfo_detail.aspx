<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/masters/BaseMaster.master"
    CodeBehind="tbcs_cusupinfo_detail.aspx.cs" Inherits="ZCZJ_DPF.Basic_Data.tbcs_cusupinfo_detail" %>

<%@ Register Src="~/Controls/UCPaging.ascx" TagName="UCPaging" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    厂商信息管理</asp:Content>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="PrimaryContent">
    <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </cc1:ToolkitScriptManager>

    <script type="text/javascript">

        window.onload = function () {
            var rbtn = document.getElementById('<%= sfjy.ClientID %>')
            //得到所有radio 
            var list= rbtn.getElementsByTagName("input"); 
            for (var i = 0; i < list.length; i++) { 
                if (list[i].checked) {
                var value = list[i].value;
                if(value=="0"){
                     document.getElementById('stop_use').innerHTML="停用";
                }
                else{
                     document.getElementById('stop_use').innerHTML="启用";           
                } 
            } 
            }
        
        }
        //初始化弹窗
        $(function() {
            $("#winadduser").show().dialog({
                title: '新增用户信息',
                width: 280,
                height: 200,
                closed: true,
                cache: false,
                modal: true,
                buttons: '#divadduser'
            });
        });
        //打开务弹窗
        function showadduser(obj) {
            var supplyname="";
            var supplyid="";
            supplyname=obj.parentNode.parentNode.getElementsByTagName("td")[1].getElementsByTagName("input")[0].value;
            supplyid=obj.parentNode.parentNode.getElementsByTagName("td")[0].getElementsByTagName("input")[0].value;
            $("#txtsupplyname").val(supplyname);
            $("#hidsupplyid").val(supplyid);
            $("#winadduser").dialog("open"); 
        }
        function saveadduser() {
            var supplyid=$("#hidsupplyid").val();
            var username = $("#txtusername").val();
            var userpassword = $("#txtuserpassword").val();
            if (username != "" && userpassword != "") {
                $.ajax({
                    type: "POST",
                    url: "BD_AjaxHandler.aspx",
                    data: { "supplyid": supplyid, "username": username,"userpassword":userpassword, "method": "Adduser" },
                    success: function(msg) {
                        if (msg == "true") {
                            alert("添加成功！");
                        } else {
                            alert("添加失败，请勿插入重复数据");

                        }
                        $("#txtusername").val("");
                        $("#txtuserpassword").val("");
                        $("#winadduser").dialog("close");
                    }
                });
            }
        }
        
        function ShowADDPage(){
          eval("<%=addcharge()%>");  
        }
    </script>

    <link href="../Assets/main.css" rel="stylesheet" type="text/css" />
    <asp:Label ID="ControlFinder" runat="server" Visible="false"></asp:Label>
    <div class="RightContent">
        <div class="box-inner">
            <div class="box_right">
            </div>
        </div>
        <div class="box-wrapper">
            <div class="box-outer" style="width: 100%;">
                <asp:Panel ID="QueryPanel" runat="server" Height="25px">
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 厂商类别：
                    <asp:DropDownList ID="dopCS_Type" runat="server" AutoPostBack="True" OnSelectedIndexChanged="dopCS_Type_OnSelectedIndexChanged">
                        <asp:ListItem Text="全部" Value="%"></asp:ListItem>
                        <asp:ListItem Text="客户" Value="1"></asp:ListItem>
                        <asp:ListItem Text="采购供应商" Value="2"></asp:ListItem>
                        <asp:ListItem Text="运输公司" Value="3"></asp:ListItem>
                        <asp:ListItem Text="技术外协分包商" Value="4"></asp:ListItem>
                        <asp:ListItem Text="生产外协分包商" Value="5"></asp:ListItem>
                        <asp:ListItem Text="原材料销售供应商" Value="6"></asp:ListItem>
                        <asp:ListItem Text="其它" Value="7"></asp:ListItem>
                    </asp:DropDownList>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;客户类别：
                    <asp:DropDownList ID="drpkhtype" runat="server" AutoPostBack="True" OnSelectedIndexChanged="drpkhtype_SelectedIndexChanged">
                        <asp:ListItem Text="-请选择-" Value="" Selected="True"></asp:ListItem>
                        <asp:ListItem Text="装备集团内" Value="1"></asp:ListItem>
                        <asp:ListItem Text="装备集团外" Value="2"></asp:ListItem>
                        <asp:ListItem Text="自营" Value="3"></asp:ListItem>
                    </asp:DropDownList>
                    <%--      &nbsp;&nbsp;&nbsp;&nbsp; 地区：<asp:DropDownList ID="dopLocation" runat="server" AutoPostBack="True"
                        OnTextChanged="dopLocation_TextChanged">
                    </asp:DropDownList>
                    省<asp:DropDownList ID="dopLocationNext" runat="server" AutoPostBack="True" OnTextChanged="dopLocationNext_TextChanged">
                    </asp:DropDownList>
                    (市/区)&nbsp;&nbsp;&nbsp;&nbsp; --%>
                    <asp:RadioButtonList ID="sfjy" runat="server" AutoPostBack="true" RepeatColumns="2"
                        RepeatLayout="Flow" OnSelectedIndexChanged="sfjy_OnSelectedIndexChanged">
                        <asp:ListItem Selected="True" Text="在用" Value="0"></asp:ListItem>
                        <asp:ListItem Text="停用" Value="1"></asp:ListItem>
                    </asp:RadioButtonList>
                    &nbsp; &nbsp;&nbsp; 名称/助记码：
                    <asp:TextBox ID="tb_csname" runat="server"></asp:TextBox>
                    <asp:Button ID="btn_select" runat="server" Text="查询" OnClick="btn_select_Click" />
                    &nbsp; &nbsp;&nbsp;
                    <asp:Button ID="btn_back" runat="server"  Text="返回" OnClick="btn_back_Click" />&nbsp;
                    &nbsp;&nbsp;
                    <asp:Button ID="bt_daochu" runat="server" OnClick="bt_daochu_click" Text="导出" />
                    &nbsp; &nbsp;&nbsp;
                    <asp:HyperLink ID="HyperLink3"  NavigateUrl="javascript:ShowADDPage();" runat="server">
                        <asp:Image ID="ImageAdd" ImageUrl="~/assets/images/Add_new_img.gif" border="0" hspace="2"
                            align="absmiddle" runat="server" />
                        添加厂商信息</asp:HyperLink>
                    &nbsp; &nbsp;&nbsp;
                    <asp:HyperLink ID="HyperLink2"   runat="server" NavigateUrl="~/Basic_Data/ZHAOBIAOUSERINFO.aspx">
                        <asp:Image ID="Image2" ImageUrl="~/assets/images/res.gif" border="0" hspace="2" align="absmiddle"
                            runat="server" />
                        查询用户信息</asp:HyperLink>
                </asp:Panel>
                <br />
                <div id="div_statistcs" style="width: 100%; height: auto; overflow: scroll; display: block;">
                    <table width="600px" align="center" cellpadding="4" cellspacing="1" class="toptable grid nowrap"
                        border="1">
                        <asp:Repeater ID="rptTBCS_CUSUPINFO" runat="server">
                            <HeaderTemplate>
                                <tr align="center" class="tableTitle">
                                    <td width="10px">
                                        <strong>序号</strong>
                                    </td>
                                    <%-- <td ><strong>公司编号</strong></td> --%>
                                    <td>
                                        <strong>公司名称</strong>
                                    </td>
                                    <td>
                                        <strong>详细地址</strong>
                                    </td>
                                    <td>
                                        <strong>助记码</strong>
                                    </td>
                                    <td>
                                        <strong>厂商类型</strong>
                                    </td>
                                    <%-- <td>
                                    <strong>供货物料代码</strong>
                                </td>--%>
                                    <td>
                                        <strong>产品所属类型</strong>
                                    </td>
                                    <td>
                                        <strong>使用状态</strong>
                                    </td>
                                    <td>
                                        <strong>联系电话</strong>
                                    </td>
                                    <td>
                                        <strong>添加时间</strong>
                                    </td>
                                    <td>
                                        <strong>客户类别</strong>
                                    </td>
                                    <td>
                                        <strong>制单人</strong>
                                    </td>
                                    <td>
                                        <strong>修改</strong>
                                    </td>
                                    <td id="stop_use">
                                        <strong></strong>
                                        <%-- <strong><%=ty_zy_change%></strong>--%>
                                    </td>
                                    <td>
                                        <strong>添加用户名和密码</strong>
                                    </td>
                                </tr>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr class="baseGadget" onmouseover="this.title='双击查看详细信息'" onclick="this.className='clickback'"
                                    onmouseout="this.className='baseGadget'" ondblclick="javascript:window.showModalDialog('tbcs_cusupinfo_show.aspx?id=<%#Eval("CS_ID")%>','','DialogWidth=600px;DialogHeight=700px;')"
                                    style="text-align: left">
                                    <td width="30px">
                                        <input id="hidcsid" runat="server" type="hidden" value='<%#Eval("CS_CODE")%>' />
                                        <asp:Label ID="lblCS_ID" runat="server" Visible="false" Text='<%#Eval("CS_ID")%>'></asp:Label>
                                        <%#Container.ItemIndex + Convert.ToInt32(Eval("RowIndex"))%>&nbsp;
                                    </td>
                                    <%--<td><%#Eval("CS_CODE")%> &nbsp;</td> --%>
                                    <td>
                                        <input id="hidcsname" runat="server" type="hidden" value='<%#Eval("CS_NAME")%>' />
                                        <%#Eval("CS_NAME")%>
                                        &nbsp;
                                    </td>
                                    <td>
                                        <%#Eval("CS_ADDRESS")%>
                                    </td>
                                    <td>
                                        <%#Eval("CS_HRCODE")%>
                                        &nbsp;
                                    </td>
                                    <td>
                                        <%#Eval("CS_TYPE")%>&nbsp;
                                    </td>
                                    <td>
                                        <%#Eval("CS_MCODE")%>&nbsp;
                                    </td>
                                    <td>
                                        <%#Eval("CS_State").ToString() == "0" ? "在用" : "停用"%>
                                    </td>
                                    <td>
                                        <%#Eval("CS_PHONO")%>&nbsp;
                                    </td>
                                    <td>
                                        <%#Eval("CS_FILLDATE")%>&nbsp;
                                    </td>
                                    <td>
                                        <%#Eval("kehutype")%>&nbsp;
                                    </td>
                                    <td>
                                        <%#Eval("CS_MANCLERK")%>&nbsp;
                                    </td>
                                    <td>
                                        <asp:HyperLink ID="HyperLink1" NavigateUrl='<%#editCs(Eval("CS_CODE").ToString()) %>'
                                            runat="server">
                                            <asp:Image ID="Image2" ImageUrl="~/assets/images/res.gif" border="0" hspace="2" align="absmiddle"
                                                runat="server" />
                                            修改</asp:HyperLink>
                                    </td>
                                    <td>
                                        <asp:ImageButton ID="Ibtn_delete" runat="server" src="../assets/icons/positive.gif"
                                            alt="停用" Style="border-width: 0px;" OnCommand="delete_Click" CommandName='<%#Eval("CS_CODE")%>'
                                            OnClientClick="return confirm(&quot;是否停用/启用该厂商？&quot;)" />
                                    </td>
                                    <td>
                                        <asp:HyperLink ID="Hypadduser" runat="server" onClick="showadduser(this)">
                                            <asp:Image ID="Image5" ImageUrl="~/Assets/images/create.gif" border="0" hspace="2"
                                                align="absmiddle" runat="server" />
                                            增加用户名和密码
                                        </asp:HyperLink>
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </table>
                    <asp:Panel ID="NoDataPanel" runat="server">
                        <p style="color: Red; text-align: center">
                            没有记录!
                        </p>
                    </asp:Panel>
                </div>
                <asp:Panel ID="Panel1" runat="server">
                    <table width="100%">
                        <tr style="text-align: right;">
                            <td>
                                <uc1:UCPaging ID="UCPaging1" runat="server" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <p style="text-align: center">
                    <asp:Label ID="lblTotalNumber" runat="server" Text=""></asp:Label></p>
            </div>
        </div>
    </div>
    <div id="winadduser" style="display: none">
        <form method="post" id="formgztask">
        <div>
            <table>
                <tr>
                    <td>
                        <strong>公司名称</strong>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    </td>
                    <td>
                        <input id="txtsupplyname" type="text" readonly="readonly" />
                        <input type="hidden" id="hidsupplyid" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <strong>用户名</strong>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    </td>
                    <td>
                        <input id="txtusername" type="text" class="easyui-validatebox" data-options="required:true" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <strong>密码</strong>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    </td>
                    <td>
                        <input id="txtuserpassword" type="text" class="easyui-validatebox" data-options="required:true" />
                    </td>
                </tr>
            </table>
        </div>
        </form>
    </div>
    <div id="divadduser" style="text-align: right; display: none">
        <a class="easyui-linkbutton" data-options="iconCls:'icon-ok',plain:true" onclick="saveadduser()">
            保存</a> <a class="easyui-linkbutton" data-options="iconCls:'icon-cancel',plain:true"
                onclick="javascript:$('#winadduser').dialog('close')">取消</a>
    </div>
</asp:Content>
