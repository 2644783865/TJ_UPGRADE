<%@ Page Language="C#" MasterPageFile="~/Masters/RightCotentMaster.master" AutoEventWireup="true"
    CodeBehind="TM_Task_View.aspx.cs" Inherits="ZCZJ_DPF.TM_Data.TM_Task_View" %>

<%@ Register Src="../Controls/UCPagingOfMS.ascx" TagName="UCPagingOfMS" TagPrefix="uc2" %>
<%@ Register Src="../Controls/UCPaging.ascx" TagName="UCPaging" TagPrefix="uc1" %>
<%@ Register Src="../Controls/UserDefinedQueryConditions.ascx" TagName="UserDefinedQueryConditions"
    TagPrefix="uc3" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="../Controls/JSRegister.ascx" TagName="JSRegister" TagPrefix="JSR" %>
<asp:Content ID="Content2" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    <asp:Label ID="labprostru" runat="server" Text=""></asp:Label></asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="PrimaryContent" runat="server">
    <JSR:JSRegister ID="JSRegister1" runat="server" />

    <script src="../JS/jquery/jquery-1.11.0.js" type="text/javascript"></script>

    <script src="../JS/BOM.js" type="text/javascript" charset="GB2312"></script>

    <script src="../JS/wxBOM.js" type="text/javascript" charset="GB2312"></script>

    <script language="javascript" type="text/javascript">

        var dbl_click = false;

        function openLink(url) {
            var date = new Date();
            var time = date.getTime();
            var returnVlue = window.showModalDialog("TM_Original_Data_Operate.aspx?NoUse=" + time + "&register=" + url, '', "dialogHeight:700px;dialogWidth:900px;status:no;scroll:yes;center:yes;toolbar=no;menubar=no");
        }

        function NodeCheck() {
            var o = window.event.srcElement;
            if (o.tagName == "INPUT" && o.type == "checkbox") //点击treeview的checkbox是触发  
            {
                var d = o.id; //获得当前checkbox的id;
                var e = d.replace("CheckBox", "Nodes"); //通过查看脚本信息,获得包含所有子节点div的id  
                var div = window.document.getElementById(e); //获得div对象  
                if (div != null)  //如果不为空则表示,存在自节点  
                {
                    var check = div.getElementsByTagName("INPUT"); //获得div中所有的已input开始的标记  
                    for (i = 0; i < check.length; i++) {
                        if (check[i].type == "checkbox") //如果是checkbox  
                        {
                            check[i].checked = o.checked; //字节点的状态和父节点的状态相同,即达到全选  
                        }
                    }
                }

                //点子节点的时候,使父节点的状态改变,即不为全选
                var divid = o.parentElement.parentElement.parentElement.parentElement.parentElement; //子节点所在的div      
                var id = divid.id.replace("Nodes", "CheckBox"); //获得根节点的id
                var div_father = window.document.getElementById(id);
                while (div_father != null) {
                    var checkbox = divid.getElementsByTagName("INPUT"); //获取所有子节点数
                    var s = 0;
                    for (i = 0; i < checkbox.length; i++) {
                        if (checkbox[i].checked)  //判断有多少子节点被选中  
                        {
                            s++;
                        }
                    }
                    if (s == checkbox.length)  //如果全部选中 或者 选择的是另外一个根节点的子节点 ，  
                    {                               //    则开始的根节点的状态仍然为选中状态  
                        window.document.getElementById(id).checked = true;
                    }
                    else {                               //否则为没选中状态  
                        window.document.getElementById(id).checked = false;
                    }
                    divid = window.document.getElementById(id).parentElement.parentElement.parentElement.parentElement.parentElement; //子节点所在的div     
                    id = divid.id.replace("Nodes", "CheckBox"); //获得根节点的id
                    div_father = window.document.getElementById(id);
                }
            }
        }


        //下查材料计划
        function MP_DownWardQuery(xuhao) {
            var date = new Date();
            var time = date.getTime();
            var returnVlue = window.showModalDialog("TM_Mp_DownWardQuery.aspx?NoUse=" + time + "&xuhao_engid=" + encodeURIComponent(xuhao), '', "dialogHeight:600px;dialogWidth:1200px;status:no;scroll:yes;center:yes;toolbar=no;menubar=no");
        }
        //下查明细
        function MS_DownWardQuery(xuhao) {
            var date = new Date();
            var time = date.getTime();
            var returnVlue = window.showModalDialog("TM_Ms_DownWardQuery.aspx?NoUse=" + time + "&xuhao_engid_table=" + encodeURIComponent(xuhao), '', "dialogHeight:600px;dialogWidth:1200px;status:no;scroll:yes;center:yes;toolbar=no;menubar=no");
        }
        //下查外协计划
        function OUT_DownWardQuery(xuhao) {
            var date = new Date();
            var time = date.getTime();
            var returnVlue = window.showModalDialog("TM_Out_DownWardQuery.aspx?NoUse=" + time + "&xuhao_engid=" + encodeURIComponent(xuhao), '', "dialogHeight:600px;dialogWidth:1200px;status:no;scroll:yes;center:yes;toolbar=no;menubar=no");
        }
        //下查代用
        function MarReplace_DownWardQuery(xuhao) {
            var date = new Date();
            var time = date.getTime();
            var returnVlue = window.showModalDialog("TM_MarReplaceView.aspx?NoUse=" + time + "&xuhao_engid_marid_table=" + encodeURIComponent(xuhao), '', "dialogHeight:600px;dialogWidth:1200px;status:no;scroll:yes;center:yes;toolbar=no;menubar=no");
        }

        function showPanel11() {
            var obj = document.getElementById("<%=Panel11.ClientID %>");
            if (obj.style.display == "none") {
                obj.style.display = "block";
            }
            else {
                obj.style.display = "none"
            }
        }

        function CheckXishu(obj) {
            var pattem = /^\d+(\.\d+)?$/; //数量验证
            if (!pattem.test(obj.value)) {
                alert("请输入正确的计划系数！！！");
                obj.value = "1";
            }
            else if (parseFloat(obj.value) < 1) {
                alert("计划系数不能小于1！！！");
                obj.value = "1";
            }
            else if (parseFloat(obj.value) > 2) {
                var con = confirm("设定的系数可能过大！！！\r\r您确认将系数设定为【" + obj.value + "】吗?");
                if (con == false) {
                    obj.value = "1";
                }
            }
        }
        function EditData(id, zongxu) {
        var myDate = new Date();
            window.open("TM_Org_Edit.aspx?tsaid=" + id + "&zongxu=" + zongxu + "&action=edit&time="+myDate.toLocaleString());
        }
        function ChangeData(id, zongxu) {
            window.open("TM_Org_Edit.aspx?tsaid=" + id + "&zongxu=" + zongxu + "&action=change");
        }
        function ChangeOneData(id, zongxu) {
            window.open("TM_Org_Edit.aspx?tsaid=" + id + "&zongxu=" + zongxu + "&action=changeOne");
        }


        //检查记录是否勾选

        function CheckSelected() {
            var table = document.getElementById("<%=GridView2.ClientID%>");

            if (table != null) {
                var tr = table.getElementsByTagName("tr");
                var checkbox;
                var bmid;
                var index = 0;

                for (i = 1; i < tr.length; i++) {
                    checkbox = tr[i].getElementsByTagName("td")[0].getElementsByTagName("input")[0];
                    if (checkbox.checked) {
                        index++;
                    }
                }

                if (index == 0) {
                    alert("请勾选要添加余量的记录！！！");
                    return false;
                }
            }
            else {
                alert("没有可操作记录！！！");
                return false;
            }
            return true;
        }


        $(function() {
            $("#<%=GridView1.ClientID %> tr").click(function() {
                //   console.log($(this).attr("class"));
                $(this).removeAttr("style");

                $(this).toggleClass("techBackColor");
            });
        });
    
    </script>

    <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </cc1:ToolkitScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="box-inner">
                <div class="box_right">
                    <div class="box-title">
                        <table width="98%">
                            <tr>
                                <td style="width: 24%">
                                    任务号:<asp:Label ID="tsaid" runat="server"></asp:Label>
                                    <input id="eng_type" type="text" value="" readonly="readonly" runat="server" style="display: none" />
                                </td>
                                <td style="width: 24%">
                                    合同号:<asp:Label ID="lab_hetongNum" runat="server"></asp:Label>
                                    <input id="hetong_num" type="text" value="" readonly="readonly" runat="server" style="display: none" />
                                </td>
                                <td style="width: 24%">
                                    项目名称:<asp:Label ID="lab_proname" runat="server"></asp:Label>
                                    <input id="pro_id" type="text" value="" readonly="readonly" runat="server" style="display: none" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </div>
            <cc1:TabContainer ID="TabContainer1" runat="server" Width="100%" ActiveTabIndex="0">
                <cc1:TabPanel ID="TabPanel1" runat="server" HeaderText="原始数据查看" TabIndex="0">
                    <ContentTemplate>
                        <asp:UpdatePanel ID="UpdatePanel12" runat="server">
                            <Triggers>
                                <asp:PostBackTrigger ControlID="btnExport" />
                            </Triggers>
                            <ContentTemplate>
                                <div>
                                    <table width="100%" align="center" cellpadding="4" cellspacing="1" class="toptable grid">
                                        <tr>
                                            <td align="center">
                                                全选/取消<input id="Checkbox2" runat="server" onclick="selectOrgAll(this);" type="checkbox" />&nbsp;&nbsp;
                                                <asp:Button ID="btnSelect_org" ToolTip="选择起止项后点击连选,起止项间所有记录选中" runat="server" OnClick="btnSelect_org_OnClick"
                                                    Text="连选" />
                                            </td>
                                            <td align="left">
                                                <asp:HyperLink ID="hylMore" CssClass="hand" onclick="showPanel11();" runat="server">
                                                    <asp:Image ID="Image2" ImageUrl="~/Assets/images/create.gif" border="0" hspace="2"
                                                        align="absmiddle" runat="server" />更多操作</asp:HyperLink>
                                            </td>
                                            <td align="right">
                                                查询类别:<asp:DropDownList ID="ddlQueryType" runat="server">
                                                    <asp:ListItem Text="-请选择-" Value="0"></asp:ListItem>
                                                    <asp:ListItem Text="总序" Value="BM_ZONGXU"></asp:ListItem>
                                                    <asp:ListItem Text="中文名称" Value="BM_CHANAME"></asp:ListItem>
                                                    <asp:ListItem Text="图号" Value="BM_TUHAO"></asp:ListItem>
                                                    <asp:ListItem Text="规格" Value="BM_GUIGE"></asp:ListItem>
                                                    <asp:ListItem Text="物料编码" Value="BM_MARID"></asp:ListItem>
                                                    <asp:ListItem Text="物料名称" Value="BM_MANAME"></asp:ListItem>
                                                    <asp:ListItem Text="物料规格" Value="BM_MAGUIGE"></asp:ListItem>
                                                    <asp:ListItem Text="国标(标准)" Value="BM_STANDARD"></asp:ListItem>
                                                    <asp:ListItem Text="材质" Value="BM_MAQUALITY"></asp:ListItem>
                                                    <asp:ListItem Text="备注" Value="BM_NOTE"></asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="txtMCorZX" runat="server"></asp:TextBox>
                                                &nbsp;&nbsp;&nbsp;
                                                <asp:Button ID="btnOrgQuery" runat="server" Width="40" Text="查询" OnClick="btnOrgQuery_OnClick" />
                                                &nbsp;&nbsp;
                                                <asp:Button ID="btnClear" runat="server" Width="40" Text="重置" OnClick="btnClear_OnClick" />
                                                &nbsp;&nbsp;
                                                <asp:Button ID="btnExport" runat="server" Width="40" Text="导出" OnClick="btnExport_OnClick" />
                                            </td>
                                            <td>
                                                <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_OnClick">
                                                    <asp:Image ID="Image11" ImageUrl="~/Assets/icon-fuction/139.gif" border="0" hspace="2"
                                                        align="absmiddle" runat="server" />原始数据修改</asp:LinkButton>
                                                &nbsp;&nbsp;&nbsp;
                                                <asp:LinkButton ID="LinkButton2" runat="server" OnClick="LinkButton2_OnClick">
                                                    <asp:Image ID="Image12" ImageUrl="~/Assets/icon-fuction/139.gif" border="0" hspace="2"
                                                        align="absmiddle" runat="server" />原始数据变更</asp:LinkButton>
                                                &nbsp;&nbsp;&nbsp;
                                                <asp:LinkButton ID="LinkButton4" runat="server" OnClick="LinkButton4_OnClick">
                                                    <asp:Image ID="Image10" ImageUrl="~/Assets/icon-fuction/139.gif" border="0" hspace="2"
                                                        align="absmiddle" runat="server" />单条变更</asp:LinkButton>
                                                &nbsp;&nbsp;&nbsp;
                                                <asp:LinkButton ID="LinkButton3" runat="server" OnClick="LinkButton3_OnClick" OnClientClick="return confirm('您确定要取消该批明细吗？');">
                                                    <asp:Image ID="Image7" ImageUrl="~/Assets/icon-fuction/139.gif" border="0" hspace="2"
                                                        align="absmiddle" runat="server" />变更取消</asp:LinkButton>
                                            </td>
                                            <td align="center">
                                                <asp:HyperLink ID="HyperLink1" CssClass="hand" runat="server">
                                                    <asp:Image ID="Image4" ImageUrl="~/Assets/icon-fuction/388.gif" border="0" hspace="2"
                                                        align="absmiddle" runat="server" />其它筛选</asp:HyperLink>
                                                <cc1:PopupControlExtender ID="PopupControlExtender1" CacheDynamicResults="false"
                                                    Position="Left" Enabled="true" runat="server" OffsetX="-1000" OffsetY="-55" TargetControlID="HyperLink1"
                                                    PopupControlID="palORG">
                                                </cc1:PopupControlExtender>
                                                <%--<cc1:ModalPopupExtender ID="ModalPopupExtenderSearch"  runat="server" Enabled="true"  TargetControlID="btnSelectOrg" PopupControlID="UpdatePanel2"  Drag="false"   Y="30" >
          </cc1:ModalPopupExtender>--%>
                                                <asp:Panel ID="palORG" Width="90%" Style="display: none; visibility: hidden; border-style: solid;
                                                    border-width: 1px; border-color: blue; background-color: Menu;" runat="server">
                                                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                        <ContentTemplate>
                                                            <table width="100%">
                                                                <tr>
                                                                    <td>
                                                                        <div style="font-family: Verdana, Helvetica, Arial, sans-serif; line-height: 17px;
                                                                            font-size: 11px; font-weight: bold; position: absolute; top: 8px; right: 10px;">
                                                                            <a onclick="document.body.click(); return false;" style="background-color: #6699CC;
                                                                                cursor: pointer; color: #FFFFFF; text-align: center; text-decoration: none; padding: 5px;"
                                                                                title="关闭">X</a>
                                                                        </div>
                                                                        <br />
                                                                        <br />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="right" class="notbrk">
                                                                        部件名称:
                                                                    </td>
                                                                    <td align="left">
                                                                        <asp:DropDownList ID="ddlpartsname" runat="server" Width="200px">
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                    <td align="right">
                                                                        <asp:Button ID="btn_OrgQuery" runat="server" UseSubmitBehavior="false" OnClick="btnOrgQuery_OnClick"
                                                                            Text="查 询" />&nbsp;&nbsp;&nbsp;
                                                                        <asp:Button ID="btnOrgClear" runat="server" UseSubmitBehavior="false" OnClick="btnOrgClear_OnClick"
                                                                            Text="清 空" />&nbsp;&nbsp;
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="right" class="notbrk">
                                                                        材料名称:
                                                                    </td>
                                                                    <td align="left">
                                                                        <asp:DropDownList ID="ddlmatername" runat="server" Width="200px">
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                    <td rowspan="9">
                                                                        <uc3:UserDefinedQueryConditions ID="udqOrg" runat="server" QueryColumnsType="TaskView"
                                                                            QueryRows="8" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="right" class="notbrk">
                                                                        材料规格:
                                                                    </td>
                                                                    <td align="left">
                                                                        <asp:DropDownList ID="ddlguige" runat="server" Width="200px">
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="right" class="notbrk">
                                                                        材料材质:
                                                                    </td>
                                                                    <td align="left">
                                                                        <asp:DropDownList ID="ddlcaizhi" runat="server" Width="200px">
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="right" class="notbrk">
                                                                        体现明细:
                                                                    </td>
                                                                    <td align="left">
                                                                        <asp:DropDownList ID="ddlOrgInMS" runat="server">
                                                                            <asp:ListItem Text="-请选择-" Value="0"></asp:ListItem>
                                                                            <asp:ListItem Text="是" Value="Y"></asp:ListItem>
                                                                            <asp:ListItem Text="否" Value="N"></asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                    <td align="right" class="notbrk">
                                                                        材料种类:
                                                                    </td>
                                                                    <td align="left">
                                                                        <asp:DropDownList ID="ddlOrgShape" runat="server">
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="right" class="notbrk">
                                                                        是否定尺:
                                                                    </td>
                                                                    <td align="left">
                                                                        <asp:DropDownList ID="ddlOrgFix" runat="server">
                                                                            <asp:ListItem Text="-请选择-" Value="0"></asp:ListItem>
                                                                            <asp:ListItem Text="是" Value="Y"></asp:ListItem>
                                                                            <asp:ListItem Text="否" Value="N"></asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="right" class="notbrk">
                                                                        制作明细:
                                                                    </td>
                                                                    <td align="left">
                                                                        <asp:DropDownList ID="ddlOrgMSState" runat="server">
                                                                            <asp:ListItem Text="-请选择-" Value="-请选择-"></asp:ListItem>
                                                                            <asp:ListItem Text="未生成" Value="WSC"></asp:ListItem>
                                                                            <asp:ListItem Text="审核中" Value="SSZ"></asp:ListItem>
                                                                            <asp:ListItem Text="通过" Value="TG"></asp:ListItem>
                                                                            <asp:ListItem Text="驳回" Value="BH"></asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                    <td align="right" class="notbrk">
                                                                        材料计划:
                                                                    </td>
                                                                    <td align="left">
                                                                        <asp:DropDownList ID="ddlOrgMPState" runat="server">
                                                                            <asp:ListItem Text="-请选择-" Value="-请选择-"></asp:ListItem>
                                                                            <asp:ListItem Text="未生成" Value="WSC"></asp:ListItem>
                                                                            <asp:ListItem Text="未提交" Value="WTJ"></asp:ListItem>
                                                                            <asp:ListItem Text="审核中" Value="SSZ"></asp:ListItem>
                                                                            <asp:ListItem Text="通过" Value="TG"></asp:ListItem>
                                                                            <asp:ListItem Text="驳回" Value="BH"></asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="right" class="notbrk">
                                                                        余量系数:
                                                                    </td>
                                                                    <td align="left">
                                                                        <asp:DropDownList ID="ddlXishu" runat="server">
                                                                            <asp:ListItem Text="-请选择-" Value="-请选择-"></asp:ListItem>
                                                                            <asp:ListItem Text="N" Value="N"></asp:ListItem>
                                                                            <asp:ListItem Text="Y" Value="Y"></asp:ListItem>
                                                                            <asp:ListItem Text="(1,1.05]" Value="1.05"></asp:ListItem>
                                                                            <asp:ListItem Text="[1.05,1.1]" Value="1.051.01"></asp:ListItem>
                                                                            <asp:ListItem Text=">1.1" Value="1.1"></asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                </tr>
                                                                <tr>
                                                                    <td align="right" class="notbrk">
                                                                        显示级数:
                                                                    </td>
                                                                    <td align="left">
                                                                        <asp:DropDownList ID="ddlOrgJishu" runat="server">
                                                                            <asp:ListItem Text="-请选择-" Value="-请选择-"></asp:ListItem>
                                                                            <asp:ListItem Text="1级" Value="0"></asp:ListItem>
                                                                            <asp:ListItem Text="2级" Value="1"></asp:ListItem>
                                                                            <asp:ListItem Text="3级" Value="2"></asp:ListItem>
                                                                            <asp:ListItem Text="4级" Value="3"></asp:ListItem>
                                                                            <asp:ListItem Text="5级" Value="4"></asp:ListItem>
                                                                            <asp:ListItem Text="6级" Value="5"></asp:ListItem>
                                                                            <asp:ListItem Text="7级" Value="6"></asp:ListItem>
                                                                            <asp:ListItem Text="8级" Value="7"></asp:ListItem>
                                                                        </asp:DropDownList>
                                                                        &nbsp;
                                                                    </td>
                                                                    <td align="right" class="notbrk">
                                                                        需用计划:
                                                                    </td>
                                                                    <td align="left">
                                                                        <asp:DropDownList ID="ddlWMarPlan" runat="server">
                                                                            <asp:ListItem Text="-请选择-" Value="-请选择-"></asp:ListItem>
                                                                            <asp:ListItem Text="提交" Value="Y"></asp:ListItem>
                                                                            <asp:ListItem Text="不提交" Value="N"></asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                            <br />
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </asp:Panel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="7">
                                                <asp:Panel ID="Panel11" runat="server" Style="display: none;">
                                                    <table width="100%">
                                                        <tr>
                                                            <td>
                                                                <asp:Button ID="btnDelete" Width="40" runat="server" Text="删除" OnClientClick="return chksure()"
                                                                    OnClick="btnDelete_Click" />
                                                            </td>
                                                            <td>
                                                                <asp:Button ID="btnWmar" Width="55" runat="server" Text="计划Y/N" OnClientClick="return chkwmar()"
                                                                    OnClick="btnWmar_Click" />
                                                            </td>
                                                            <%--<td align="right">
                                                                替换/追加:
                                                            </td>
                                                            <td style="width: 150px;">
                                                                <asp:DropDownList ID="ddlRepType" runat="server">
                                                                    <asp:ListItem Text="图号" Value="BM_TUHAO" Selected="True"></asp:ListItem>
                                                                    <asp:ListItem Text="备注" Value="BM_NOTE"></asp:ListItem>
                                                                    <asp:ListItem Text="材料种类" Value="BM_MASHAPE"></asp:ListItem>
                                                                </asp:DropDownList>
                                                                <asp:DropDownList ID="ddlReplaceType" runat="server">
                                                                    <asp:ListItem Text="替换" Value="Replace" Selected="True"></asp:ListItem>
                                                                    <asp:ListItem Text="追加" Value="Add"></asp:ListItem>
                                                                </asp:DropDownList>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox ID="txtStart" runat="server"></asp:TextBox>>><asp:TextBox ID="txtEnd"
                                                                    runat="server"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:Button ID="btnSave" runat="server" Width="60" OnClick="btnSave_Replace" OnClientClick="return chkreplace();"
                                                                    Text="替换/追加" />
                                                            </td>--%>
                                                            <td style="width: 20px">
                                                                <asp:Button ID="btnCalWeight" Width="55" runat="server" Text="计算重量" OnClick="btnCalWeight_Click" />
                                                            </td>
                                                            <td style="width: 20px">
                                                                图纸总重：
                                                            </td>
                                                            <td style="width: 30px">
                                                                <input type="text" id="txtTotalTu" runat="server" />
                                                            </td>
                                                            <td style="width: 20px">
                                                                材料总重：
                                                            </td>
                                                            <td>
                                                                <input id="txtTotalMP" type="text" runat="server" />
                                                            </td>
                                                            <td style="width: 20px">
                                                                <asp:DropDownList runat="server" ID="ddlChangeItem">
                                                                    <asp:ListItem Text="图号" Value="BM_TUHAO" Selected="True"></asp:ListItem>
                                                                </asp:DropDownList>
                                                            </td>
                                                            <td style="width: 20px">
                                                                <input id="txtEquipMap" type="text" runat="server" />
                                                            </td>
                                                            <td style="width: 15px">
                                                                改为
                                                            </td>
                                                            <td style="width: 20px">
                                                                <input id="txtChangeMap" type="text" runat="server" />
                                                            </td>
                                                            <td>
                                                                <asp:Button runat="server" ID="btnChangeMap" OnClick="btnChangeMap_Click" Text="替换" />
                                                            </td>
                                                            <td>
                                                                <input id="Button4" type="button" style="width: 40px" onclick="showPanel11();" value="隐藏" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </asp:Panel>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <asp:CheckBoxList ID="cklHiddenShow" runat="server" RepeatDirection="Horizontal"
                            RepeatColumns="17" OnSelectedIndexChanged="cklHiddenShow_OnSelectedIndexChanged"
                            AutoPostBack="true" RepeatLayout="Table">
                        </asp:CheckBoxList>
                        <asp:Panel ID="NoDataPanel1" runat="server" HorizontalAlign="Center" Font-Size="Large">
                            没有记录!</asp:Panel>
                        <div id="divGvData" runat="server" style="position: relative; top: 0px; left: 0px;
                            overflow: scroll; width: 100%; height: 480px;" onscroll="funGrilViewScroll(); return false;">
                            <asp:GridView ID="GridView1" Width="100%" CssClass="toptable grid" Style="white-space: normal"
                                runat="server" AutoGenerateColumns="False" CellPadding="2" ForeColor="#333333"
                                DataKeyNames="BM_XUHAO" OnRowDataBound="GridView1_RowDataBound" AllowPaging="False">
                                <RowStyle BackColor="#EFF3FB" />
                                <HeaderStyle />
                                <Columns>
                                    <asp:TemplateField HeaderStyle-Wrap="false">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="CheckBox1" runat="server" Width="10px" onclick="chkDelete();setMap(this)"
                                                CssClass="checkBoxCss" />
                                            <input id="Hidden1" type="hidden" runat="server" />
                                        </ItemTemplate>
                                        <ItemStyle Width="10px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="行号" HeaderStyle-Wrap="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblIndex" runat="server" Text="<%# Convert.ToInt32(Container.DataItemIndex +1) %>" />
                                            <asp:HiddenField ID="hdfOrgState" runat="server" Value='<%#Eval("BM_MPSTATE").ToString()+"-"+Eval("BM_MPSTATUS").ToString()+"-"+Eval("BM_MSSTATE").ToString()+"-"+Eval("BM_MSSTATUS").ToString()+"-"+Eval("BM_MARID").ToString()+"-"+Eval("BM_WMARPLAN").ToString()+"-"+Eval("BM_ISMANU").ToString() %>' />
                                            <input type="hidden" runat="server" value='<%# Eval("BM_ID") %>' id="hidBmId" />
                                            <%--材料计划状态、材料变更状态、明细状态、明细变更状态、物料编码、是否提材料计划--%>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                    </asp:TemplateField>
                                    <asp:BoundField HeaderText="C" ItemStyle-HorizontalAlign="Center" />
                                    <asp:BoundField HeaderText="Z" ItemStyle-HorizontalAlign="Center" />
                                    <asp:BoundField DataField="BM_TUHAO" HeaderText="图号(标识号)" HeaderStyle-Wrap="false">
                                        <ItemStyle HorizontalAlign="Left" Wrap="false" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="BM_MARID" HeaderText="物料编码" HeaderStyle-Wrap="false">
                                        <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="BM_ZONGXU" HeaderText="总序" HeaderStyle-Wrap="false">
                                        <ItemStyle HorizontalAlign="Left" Wrap="false" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="BM_CHANAME" HeaderText="中文名称" HeaderStyle-Wrap="false">
                                        <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="BM_MAGUIGE" HeaderText="材料规格" HeaderStyle-Wrap="false">
                                        <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="BM_MAQUALITY" HeaderText="材质" HeaderStyle-Wrap="false">
                                        <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="NUMBER" HeaderText="单台数量|总数量|计划数量" HeaderStyle-Wrap="false">
                                        <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="BM_TUUNITWGHT" DataFormatString="{0:F2}" HeaderText="图纸单重(kg)"
                                        HeaderStyle-Wrap="false">
                                        <ItemStyle HorizontalAlign="Center" Wrap="false" BackColor="Yellow" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="BM_TUTOTALWGHT" DataFormatString="{0:F2}" HeaderText="图纸总重(kg)"
                                        HeaderStyle-Wrap="false">
                                        <ItemStyle HorizontalAlign="Center" Wrap="false" BackColor="Yellow" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="BM_MASHAPE" HeaderText="材料种类" HeaderStyle-Wrap="false">
                                        <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="BM_TECHUNIT" HeaderText="技术单位" HeaderStyle-Wrap="false">
                                        <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="BM_YONGLIANG" HeaderText="材料用量" HeaderStyle-Wrap="false">
                                        <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="BM_MABGZMY" DataFormatString="{0:F2}" HeaderText="面域(m2)"
                                        HeaderStyle-Wrap="false">
                                        <ItemStyle HorizontalAlign="Center" BackColor="Yellow" Wrap="false" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="BM_MPMY" DataFormatString="{0:F2}" HeaderText="计划面域(m2)"
                                        HeaderStyle-Wrap="false">
                                        <ItemStyle HorizontalAlign="Center" BackColor="Yellow" Wrap="false" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="BM_MATOTALLGTH" DataFormatString="{0:F2}" HeaderText="材料总长(mm)"
                                        HeaderStyle-Wrap="false">
                                        <ItemStyle HorizontalAlign="Center" BackColor="Yellow" Wrap="false" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="BM_MAUNITWGHT" DataFormatString="{0:F2}" HeaderText="材料单重(kg)"
                                        HeaderStyle-Wrap="false">
                                        <ItemStyle HorizontalAlign="Center" BackColor="Yellow" Wrap="false" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="BM_MATOTALWGHT" DataFormatString="{0:F2}" HeaderText="材料总重(kg)"
                                        HeaderStyle-Wrap="false">
                                        <ItemStyle HorizontalAlign="Center" BackColor="Yellow" Wrap="false" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="BM_MALENGTH" DataFormatString="{0:F2}" HeaderText="长度(mm)"
                                        HeaderStyle-Wrap="false">
                                        <ItemStyle HorizontalAlign="Center" BackColor="Yellow" Wrap="false" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="BM_MAWIDTH" DataFormatString="{0:F2}" HeaderText="宽度(mm)"
                                        HeaderStyle-Wrap="false">
                                        <ItemStyle HorizontalAlign="Center" BackColor="Yellow" Wrap="false" />
                                    </asp:BoundField>
                                    <asp:TemplateField HeaderText="下料备注" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="300px">
                                        <ItemTemplate>
                                            <div style="width: 100px">
                                                <label>
                                                    <%# Eval("BM_NOTE")%></label></div>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="BM_XIALIAO" HeaderText="下料方式" HeaderStyle-Wrap="false">
                                        <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                    </asp:BoundField>
                                    <asp:TemplateField HeaderText="工艺流程" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ID="lblProcess" runat="server" Text='<%#Eval("BM_PROCESS") %>' Width="60px"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="BM_KU" HeaderText="库" HeaderStyle-Wrap="false">
                                        <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                    </asp:BoundField>
                                    <asp:TemplateField HeaderText="备注" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <div style="width: 100px">
                                                <label>
                                                    <%# Eval("BM_ALLBEIZHU")%></label></div>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="BM_THRYWGHT" DataFormatString="{0:F2}" HeaderText="理论重量"
                                        HeaderStyle-Wrap="false">
                                        <ItemStyle HorizontalAlign="Center" BackColor="Silver" Wrap="false" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="BM_STANDARD" HeaderText="国标" HeaderStyle-Wrap="false">
                                        <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="BM_ISMANU" HeaderText="制作明细" HeaderStyle-Wrap="false">
                                        <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="BM_FIXEDSIZE" HeaderText="是否定尺" HeaderStyle-Wrap="false">
                                        <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="BM_WMARPLAN" HeaderText="材料计划" HeaderStyle-Wrap="false">
                                        <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                    </asp:BoundField>
                                </Columns>
                                <PagerStyle CssClass="bomcolor" ForeColor="#EEF7FD" HorizontalAlign="Center" />
                                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                <HeaderStyle BackColor="#B9D3EE" Font-Bold="True" ForeColor="#1E5C95" />
                                <EditRowStyle BackColor="#2461BF" />
                                <AlternatingRowStyle BackColor="White" />
                            </asp:GridView>
                        </div>
                        <uc1:UCPaging ID="UCPagingOrg" runat="server" />
                    </ContentTemplate>
                </cc1:TabPanel>
                <cc1:TabPanel ID="TabPanel2" runat="server" HeaderText="原始数据>>材料计划" TabIndex="1">
                    <ContentTemplate>
                        <div>
                            <table width="100%" align="center" cellpadding="4" cellspacing="1" class="toptable grid">
                                <tr>
                                    <td align="center">
                                        全选/取消<input id="ckbSelectAll_mp" runat="server" onclick="selectAll(this);" type="checkbox" />&nbsp;&nbsp;
                                        <asp:Button ID="btnSelect_mp" ToolTip="选择起止项后点击连选,起止项间所有记录选中" runat="server" OnClick="btnSelect_mp_OnClick"
                                            Text="连选" />
                                    </td>
                                    <td align="right" class="notbrk">
                                        材料计划类别:
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlmptype" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlnamewithcheckboxlist_SelectedIndexChanged">
                                            <asp:ListItem Text="全部提交" Value="所有类型"></asp:ListItem>
                                            <asp:ListItem Text="钢材" Value="钢材"></asp:ListItem>
                                            <asp:ListItem Text="标准件" Value="采"></asp:ListItem>
                                            <asp:ListItem Text="采购成品" Value="采购成品"></asp:ListItem>
                                            <asp:ListItem Text="非金属" Value="非"></asp:ListItem>
                                            <asp:ListItem Text="锻件" Value="锻件"></asp:ListItem>
                                            <asp:ListItem Text="铸件" Value="铸件"></asp:ListItem>
                                        </asp:DropDownList>
                                        <span style="color: Red">*</span>
                                    </td>
                                    <td align="center">
                                        <asp:HyperLink ID="HyperLink4" CssClass="hand" runat="server">
                                            <asp:Image ID="Image3" ImageUrl="~/Assets/icons/forum.gif" border="0" hspace="2"
                                                align="absmiddle" runat="server" />提交条件</asp:HyperLink>
                                        <cc1:PopupControlExtender ID="PopupControlExtender4" CacheDynamicResults="false"
                                            Position="Left" Enabled="true" runat="server" OffsetX="-50" OffsetY="-100" TargetControlID="HyperLink4"
                                            PopupControlID="Panel3">
                                        </cc1:PopupControlExtender>
                                        <asp:Panel ID="Panel3" Style="display: none; width: 300px; visibility: hidden; border-style: solid;
                                            border-width: 1px; border-color: blue; background-color: Menu;" runat="server">
                                            <asp:UpdatePanel ID="UpdatePanel5" UpdateMode="Always" runat="server">
                                                <ContentTemplate>
                                                    <table width="100%" style="margin-left: -350px">
                                                        <tr>
                                                            <td>
                                                                <div style="font-family: Verdana, Helvetica, Arial, sans-serif; line-height: 17px;
                                                                    font-size: 11px; font-weight: bold; position: absolute; top: 8px; right: 10px;">
                                                                    <a onclick="document.body.click(); return false;" style="background-color: #6699CC;
                                                                        cursor: pointer; color: #FFFFFF; text-align: center; text-decoration: none; padding: 5px;"
                                                                        title="关闭">X</a>
                                                                </div>
                                                                <br />
                                                                <br />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left" class="notbrk">
                                                                图号：
                                                            </td>
                                                            <td align="left">
                                                                <asp:DropDownList runat="server" ID="ddlShebei" Width="70px">
                                                                </asp:DropDownList>
                                                                <span style="color: Red">*</span>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right" class="notbrk">
                                                                提交类别:
                                                            </td>
                                                            <td align="left">
                                                                <asp:DropDownList ID="ddlChange" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlnamewithcheckboxlist_SelectedIndexChanged">
                                                                    <asp:ListItem Text="-请选择-" Value="-请选择-" Selected="True"></asp:ListItem>
                                                                    <asp:ListItem Text="正常" Value="0"></asp:ListItem>
                                                                    <asp:ListItem Text="变更" Value="1"></asp:ListItem>
                                                                    <asp:ListItem Text="取消" Value="2"></asp:ListItem>
                                                                </asp:DropDownList>
                                                                <span style="color: Red">*</span>
                                                            </td>
                                                        </tr>
                                                        <tr runat="server" id="tr5">
                                                            <td>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TreeView ID="TreeMp" runat="server" Font-Name="Arial" RootNodeStyle-Font-Bold="true"
                                                                    SelectedNodeStyle-ForeColor="Red" ForeColor="Black" PopulateNodesFromClsient="true"
                                                                    ShowLines="true" NodeIndent="20" ToolTip="序号-总序-名称" onclick="NodeCheck();">
                                                                </asp:TreeView>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                    <br />
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </asp:Panel>
                                    </td>
                                    <td align="center">
                                        <asp:HyperLink ID="HyperLink2" CssClass="hand" runat="server">
                                            <asp:Image ID="Image5" ImageUrl="~/Assets/icon-fuction/388.gif" border="0" hspace="2"
                                                align="absmiddle" runat="server" />其它筛选</asp:HyperLink>
                                        <cc1:PopupControlExtender ID="PopupControlExtender2" CacheDynamicResults="false"
                                            Position="Left" Enabled="true" runat="server" OffsetX="-900" OffsetY="-55" TargetControlID="HyperLink2"
                                            PopupControlID="palMp">
                                        </cc1:PopupControlExtender>
                                        <asp:Panel ID="palMp" Width="80%" Style="display: none; visibility: hidden; border-style: solid;
                                            border-width: 1px; border-color: blue; background-color: Menu;" runat="server">
                                            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                                <Triggers>
                                                    <asp:PostBackTrigger ControlID="btnMPQuery" />
                                                </Triggers>
                                                <ContentTemplate>
                                                    <table width="100%">
                                                        <tr>
                                                            <td>
                                                                <div style="font-family: Verdana, Helvetica, Arial, sans-serif; line-height: 17px;
                                                                    font-size: 11px; font-weight: bold; position: absolute; top: 8px; right: 10px;">
                                                                    <a onclick="document.body.click(); return false;" style="background-color: #6699CC;
                                                                        cursor: pointer; color: #FFFFFF; text-align: center; text-decoration: none; padding: 5px;"
                                                                        title="关闭">X</a>
                                                                </div>
                                                                <br />
                                                                <br />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right" class="notbrk">
                                                                部件名称:
                                                            </td>
                                                            <td align="left">
                                                                <asp:DropDownList ID="ddlbjname" runat="server" Width="200px">
                                                                </asp:DropDownList>
                                                            </td>
                                                            <td align="right" class="notbrk">
                                                                材料名称:
                                                            </td>
                                                            <td align="left" colspan="3">
                                                                <asp:DropDownList ID="ddlname" runat="server" Width="200px">
                                                                    <asp:ListItem Text="-请选择-" Value="0" Selected="True"></asp:ListItem>
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right" class="notbrk">
                                                                材料类型:
                                                            </td>
                                                            <td align="left">
                                                                <asp:DropDownList ID="ddlmpxz" runat="server">
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="6" align="right">
                                                                <hr />
                                                                <asp:Button ID="btnMPQuery" runat="server" UseSubmitBehavior="false" OnClick="ddlname_SelectedIndexChanged"
                                                                    Text="查 询" />
                                                                &nbsp;&nbsp;<asp:Button ID="btnMPClear" runat="server" UseSubmitBehavior="false"
                                                                    OnClick="btnMPClear_OnClick" Text="清 空" />&nbsp;&nbsp;
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="6" align="left" style="width: 98%">
                                                                <uc3:UserDefinedQueryConditions runat="server" ID="udqcMp" QueryColumnsType="TaskView"
                                                                    QueryRows="8" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="6">
                                                                <hr />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right">
                                                                显示级数:
                                                            </td>
                                                            <td align="left">
                                                                <asp:DropDownList ID="ddlJishuMp" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddlJishuMp_OnSelectedIndexChanged">
                                                                    <asp:ListItem Text="1级" Value="1"></asp:ListItem>
                                                                    <asp:ListItem Text="2级" Value="2"></asp:ListItem>
                                                                    <asp:ListItem Text="3级" Value="3"></asp:ListItem>
                                                                    <asp:ListItem Text="4级" Value="4"></asp:ListItem>
                                                                    <asp:ListItem Text="5级" Value="5"></asp:ListItem>
                                                                    <asp:ListItem Text="6级" Value="6"></asp:ListItem>
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="6" align="left">
                                                                <asp:TreeView ID="TreeViewMp" runat="server" Font-Name="Arial" RootNodeStyle-Font-Bold="true"
                                                                    SelectedNodeStyle-ForeColor="Red" ForeColor="Black" PopulateNodesFromClsient="true"
                                                                    ShowLines="true" NodeIndent="20" ToolTip="序号-总序-名称" onclick="NodeCheck();">
                                                                </asp:TreeView>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="6">
                                                                <hr />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left" colspan="6">
                                                                物料名称:
                                                            </td>
                                                            <tr>
                                                                <td colspan="6" align="left">
                                                                    <asp:CheckBoxList ID="cblMarid" runat="server" RepeatColumns="4" RepeatDirection="Horizontal"
                                                                        RepeatLayout="Table">
                                                                    </asp:CheckBoxList>
                                                                </td>
                                                            </tr>
                                                    </table>
                                                    <br />
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </asp:Panel>
                                    </td>
                                    <td align="right">
                                        <asp:CheckBox ID="ckbMpMore" runat="server" ToolTip="计划数量大于100条，一次性全部生成计划" OnCheckedChanged="ckbMpMore_OnCheckedChanged"
                                            AutoPostBack="true" />大于100条 &nbsp;&nbsp;&nbsp;&nbsp;
                                        <asp:Button ID="mpsubmit" runat="server" Text="生成材料计划" OnClientClick="return mpId(this)"
                                            OnClick="mpsubmit_Click" />&nbsp;&nbsp;&nbsp;<asp:CheckBox ID="chkiffast" runat="server" />是否加急物料
                                        <asp:Button ID="mpChange" runat="server" Text="材料计划变更" OnClientClick="return mpId(this)"
                                            OnClick="mpChange_Click" />
                                        <asp:Button ID="mpCutDown" runat="server" Text="取消材料计划" OnClientClick="return mpId(this)"
                                            OnClick="mpCutDown_Click" />
                                        <asp:LinkButton ID="lkbtnToMpPage" runat="server" OnClick="lkbtnToMpPage_OnClick"
                                            Visible="false">
                                            <asp:Image ID="Image9" ImageUrl="~/Assets/icon-fuction/139.gif" border="0" hspace="2"
                                                align="absmiddle" runat="server" />转到计划生成页面</asp:LinkButton>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <asp:Panel ID="NoDataPanel2" runat="server" HorizontalAlign="Center" Font-Size="Large">
                            没有记录!</asp:Panel>
                        <asp:GridView ID="GridView2" OnRowDataBound="GridView2_OnRowDataBound" Width="100%"
                            CssClass="toptable grid" runat="server" AutoGenerateColumns="False" CellPadding="2"
                            ForeColor="#333333">
                            <RowStyle BackColor="#EFF3FB" />
                            <Columns>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="CheckBox1" runat="server" Width="10px" CssClass="checkBoxCss" />
                                    </ItemTemplate>
                                    <ItemStyle Width="10px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="行号" HeaderStyle-Wrap="false">
                                    <ItemTemplate>
                                        <asp:HiddenField ID="hdfMPtate" Value='<%#Eval("BM_MPSTATE").ToString() %>' runat="server" />
                                        <asp:HiddenField ID="hdfMPChg" Value='<%#Eval("BM_MSSTATUS").ToString() %>' runat="server" />
                                        <asp:Label ID="lblIndex" runat="server" Text="<%# Convert.ToInt32(Container.DataItemIndex +1) %>" />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="30px" />
                                </asp:TemplateField>
                                <%--  <asp:BoundField DataField="BM_XUHAO" HeaderText="序号"  HeaderStyle-Wrap="false">
                <ItemStyle HorizontalAlign="Left" Wrap="false" />
            </asp:BoundField>--%>
                                <asp:BoundField DataField="BM_TUHAO" HeaderText="图号(标识号)" HeaderStyle-Wrap="false">
                                    <ItemStyle HorizontalAlign="Left" Wrap="false" />
                                </asp:BoundField>
                                <asp:BoundField DataField="BM_MARID" HeaderText="物料编码" HeaderStyle-Wrap="false">
                                    <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                </asp:BoundField>
                                <asp:BoundField DataField="BM_ZONGXU" HeaderText="总序" HeaderStyle-Wrap="false">
                                    <ItemStyle HorizontalAlign="Left" Wrap="false" />
                                </asp:BoundField>
                                <asp:BoundField DataField="BM_CHANAME" HeaderText="材料名称" HeaderStyle-Wrap="false">
                                    <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                </asp:BoundField>
                                <asp:BoundField DataField="BM_MAGUIGE" HeaderText="材料规格" HeaderStyle-Wrap="false">
                                    <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                </asp:BoundField>
                                <asp:BoundField DataField="BM_MAQUALITY" HeaderText="材质" HeaderStyle-Wrap="false">
                                    <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                </asp:BoundField>
                                <asp:BoundField DataField="BM_PNUMBER" HeaderText="计划数量" HeaderStyle-Wrap="false">
                                    <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                </asp:BoundField>
                                <asp:BoundField DataField="BM_TECHUNIT" HeaderText="单位" HeaderStyle-Wrap="false">
                                    <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                </asp:BoundField>
                                <asp:BoundField DataField="BM_YONGLIANG" HeaderText="材料用量" HeaderStyle-Wrap="false">
                                    <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                </asp:BoundField>
                                <asp:BoundField DataField="BM_MAUNITWGHT" DataFormatString="{0:F2}" HeaderText="材料单重(kg)"
                                    HeaderStyle-Wrap="false">
                                    <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                </asp:BoundField>
                                <asp:BoundField DataField="BM_MATOTALWGHT" DataFormatString="{0:F2}" HeaderText="材料总重(kg)"
                                    HeaderStyle-Wrap="false">
                                    <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                </asp:BoundField>
                                <asp:BoundField DataField="BM_MASHAPE" HeaderText="材料种类" HeaderStyle-Wrap="false">
                                    <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                </asp:BoundField>
                                <asp:BoundField DataField="BM_MALENGTH" DataFormatString="{0:F2}" HeaderText="长度(mm)"
                                    HeaderStyle-Wrap="false">
                                    <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                </asp:BoundField>
                                <asp:BoundField DataField="BM_MAWIDTH" DataFormatString="{0:F2}" HeaderText="宽度(mm)"
                                    HeaderStyle-Wrap="false">
                                    <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                </asp:BoundField>
                                <asp:BoundField DataField="BM_MPMY" DataFormatString="{0:F2}" HeaderText="计划面域(m2)"
                                    HeaderStyle-Wrap="false">
                                    <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                </asp:BoundField>
                                <asp:BoundField DataField="BM_MATOTALLGTH" DataFormatString="{0:F2}" HeaderText="材料总长(mm)"
                                    HeaderStyle-Wrap="false">
                                    <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                </asp:BoundField>
                                <asp:BoundField DataField="BM_THRYWGHT" DataFormatString="{0:F2}" HeaderText="理论重量"
                                    HeaderStyle-Wrap="false">
                                    <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                </asp:BoundField>
                                <%-- <asp:BoundField DataField="BM_PROCESS"    HeaderText="工艺流程"  HeaderStyle-Wrap="false">
                <ItemStyle HorizontalAlign="Center" Wrap="false" />
            </asp:BoundField>
            <asp:BoundField DataField="BM_KU"    HeaderText="库"  HeaderStyle-Wrap="false">
                <ItemStyle HorizontalAlign="Center" Wrap="false" />
            </asp:BoundField>--%>
                                <asp:BoundField HeaderText="是否定尺" HeaderStyle-Wrap="false" DataField="BM_FIXEDSIZE"
                                    ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField DataField="BM_STANDARD" HeaderText="国标">
                                    <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                </asp:BoundField>
                                <%--  <asp:BoundField HeaderText="关键部件"    HeaderStyle-Wrap="false" DataField="BM_KEYCOMS" ItemStyle-HorizontalAlign="Center" />--%>
                                <asp:TemplateField HeaderText="变更状态" ItemStyle-HorizontalAlign="Center" HeaderStyle-Wrap="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblMPChangeState" CssClass="notbrk" runat="server" Text='<%#Eval("BM_MPSTATUS").ToString()=="1"?"删除":Eval("BM_MPSTATUS").ToString()=="2"?"增加":Eval("BM_MPSTATUS").ToString()=="3"?"修改":"正常" %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <%-- <asp:BoundField DataField="BM_GUIGE"    HeaderText="规格"  HeaderStyle-Wrap="false">
                <ItemStyle HorizontalAlign="Center" Wrap="false" />
            </asp:BoundField>--%>
                                <asp:TemplateField HeaderText="备注" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblAllbeizhu" CssClass="notbrk" runat="server" Text='<%#Eval("BM_ALLBEIZHU") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <%--  <asp:BoundField HeaderText="备注" DataField="BM_ALLBEIZHU" ItemStyle-HorizontalAlign="Center"
                                    ItemStyle-Wrap="false" />--%>
                                <asp:BoundField HeaderText="库" DataField="BM_KU" ItemStyle-HorizontalAlign="Center"
                                    ItemStyle-Wrap="false" />
                                <asp:BoundField HeaderText="旧序" DataField="BM_OLDINDEX" ItemStyle-HorizontalAlign="Center"
                                    ItemStyle-Wrap="false" Visible="false" />
                            </Columns>
                            <PagerStyle CssClass="bomcolor" ForeColor="#EEF7FD" HorizontalAlign="Center" />
                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                            <HeaderStyle BackColor="#B9D3EE" Font-Bold="True" ForeColor="#1E5C95" />
                            <EditRowStyle BackColor="#2461BF" />
                            <AlternatingRowStyle BackColor="White" />
                        </asp:GridView>
                        <uc1:UCPaging ID="UCPagingMP" runat="server" />
                        <asp:Panel ID="Panel1" runat="server" Font-Size="Large" BackColor="#B3CDE8">
                            备注：红色标志为变更修改过的材料!橙色标志为漏提的材料!灰色标志为待删除的材料!<br />
                        </asp:Panel>
                    </ContentTemplate>
                </cc1:TabPanel>
                <cc1:TabPanel ID="TabPanel3" runat="server" HeaderText="原始数据>>制作明细" TabIndex="2">
                    <ContentTemplate>
                        <div>
                            <asp:UpdatePanel ID="UpdatePanel123" runat="server">
                                <Triggers>
                                    <%--    <asp:PostBackTrigger ControlID="btnExportMsNew" />--%>
                                </Triggers>
                                <ContentTemplate>
                                    <table width="100%" align="center" cellpadding="4" cellspacing="1" class="toptable grid">
                                        <tr>
                                            <td align="right">
                                                部件名称：
                                            </td>
                                            <td align="left">
                                                <asp:DropDownList ID="ddpMSbjName" runat="server" Width="200px">
                                                </asp:DropDownList>
                                            </td>
                                            <td align="right">
                                                是否体现:
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlMSinMS" runat="server">
                                                    <asp:ListItem Text="-请选择-" Value="-请选择-"></asp:ListItem>
                                                    <asp:ListItem Text="是" Value="Y"></asp:ListItem>
                                                    <asp:ListItem Text="否" Value="N"></asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td align="center">
                                                <asp:Button ID="btnExportMs" runat="server" Text="导出明细" OnClick="btnExportMs_OnClick"
                                                    OnClientClick="return confirm('以下是从原始数据中导出的制作明细，确认导出吗？\r\r提示:按您选择的查询条件导出！！！');" />
                                            </td>
                                            <td>
                                                <asp:Button ID="Button5" runat="server" Text="查 询" OnClick="ddpMSbjName_OnSelectedIndexChanged" />
                                            </td>
                                            <td align="center">
                                                <asp:HyperLink ID="HyperLink3" CssClass="hand" runat="server">
                                                    <asp:Image ID="Image6" ImageUrl="~/Assets/icons/forum.gif" border="0" hspace="2"
                                                        align="absmiddle" runat="server" />提交条件</asp:HyperLink>
                                                <cc1:PopupControlExtender ID="PopupControlExtender3" CacheDynamicResults="false"
                                                    Position="Left" Enabled="true" runat="server" OffsetX="-360" OffsetY="-55" TargetControlID="HyperLink3"
                                                    PopupControlID="palMs">
                                                </cc1:PopupControlExtender>
                                                <asp:Panel ID="palMs" Style="display: none; width: 750px; visibility: hidden; border-style: solid;
                                                    border-width: 1px; border-color: blue; background-color: Menu;" runat="server">
                                                    <asp:UpdatePanel ID="UpdatePanel4" UpdateMode="Always" runat="server">
                                                        <ContentTemplate>
                                                            <table width="100%" style="margin-left: -360px">
                                                                <tr>
                                                                    <td>
                                                                        <div style="font-family: Verdana, Helvetica, Arial, sans-serif; line-height: 17px;
                                                                            font-size: 11px; font-weight: bold; text-align: center; position: absolute; top: 8px;
                                                                            right: 100px;">
                                                                            <a onclick="document.body.click(); return false;" style="background-color: #6699CC;
                                                                                cursor: pointer; color: #FFFFFF; text-decoration: none; padding: 5px;" title="关闭">
                                                                                X</a>
                                                                        </div>
                                                                        <br />
                                                                        <br />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="right" style="width: 10%;" class="notbrk">
                                                                        明细状态:
                                                                    </td>
                                                                    <td align="left">
                                                                        <asp:DropDownList ID="ddlMSChange" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddpMSbjName_OnSelectedIndexChanged">
                                                                            <asp:ListItem Text="-请选择-" Value="-请选择-"></asp:ListItem>
                                                                            <asp:ListItem Text="变更" Value="1"></asp:ListItem>
                                                                            <asp:ListItem Text="正常" Value="0"></asp:ListItem>
                                                                        </asp:DropDownList>
                                                                        <span style="color: Red">*</span>
                                                                    </td>
                                                                </tr>
                                                                <tr runat="server" id="tr_1">
                                                                    <td align="right" class="notbrk">
                                                                        提交方式:
                                                                    </td>
                                                                    <td align="left">
                                                                        <asp:DropDownList ID="ddlAdjustPart" runat="server">
                                                                            <asp:ListItem Text="自定义提交项" Value="0"></asp:ListItem>
                                                                            <asp:ListItem Text="全部提交" Value="1"></asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                </tr>
                                                                <tr runat="server" id="tr_4">
                                                                    <td>
                                                                    </td>
                                                                    <td align="left">
                                                                        <asp:TreeView ID="TreeView1" runat="server" Font-Name="Arial" RootNodeStyle-Font-Bold="true"
                                                                            SelectedNodeStyle-ForeColor="Red" ForeColor="Black" PopulateNodesFromClsient="true"
                                                                            ShowLines="true" NodeIndent="20" ToolTip="总序-名称" onclick="NodeCheck();">
                                                                        </asp:TreeView>
                                                                    </td>
                                                                </tr>
                                                                <tr runat="server" id="tr_5" visible="false">
                                                                    <td>
                                                                    </td>
                                                                    <td align="left">
                                                                        <asp:TreeView ID="TreeViewMsChange" runat="server" Font-Name="Arial" RootNodeStyle-Font-Bold="true"
                                                                            SelectedNodeStyle-ForeColor="Red" ForeColor="Black" PopulateNodesFromClsient="true"
                                                                            ShowLines="true" NodeIndent="20" ToolTip="总序-名称" onclick="NodeCheck();">
                                                                        </asp:TreeView>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                            <br />
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </asp:Panel>
                                            </td>
                                            <td>
                                                <asp:HyperLink ID="HyperLink5" CssClass="hand" runat="server">
                                                    <asp:Image ID="Image8" ImageUrl="~/Assets/icon-fuction/388.gif" border="0" hspace="2"
                                                        align="absmiddle" runat="server" />其它筛选</asp:HyperLink>
                                                <cc1:PopupControlExtender ID="PopupControlExtender5" CacheDynamicResults="false"
                                                    Position="Left" Enabled="true" runat="server" OffsetX="-300" OffsetY="-55" TargetControlID="HyperLink5"
                                                    PopupControlID="palMS3">
                                                </cc1:PopupControlExtender>
                                                <asp:Panel ID="palMS3" Style="display: none; visibility: hidden; border-style: solid;
                                                    border-width: 1px; border-color: blue; background-color: Menu;" runat="server">
                                                    <asp:UpdatePanel ID="UpdatePanel6" UpdateMode="Always" runat="server">
                                                        <ContentTemplate>
                                                            <table>
                                                                <tr>
                                                                    <td>
                                                                        <div style="font-family: Verdana, Helvetica, Arial, sans-serif; line-height: 17px;
                                                                            font-size: 11px; font-weight: bold; position: absolute; top: 8px; right: 10px;">
                                                                            <a onclick="document.body.click(); return false;" style="background-color: #6699CC;
                                                                                cursor: pointer; color: #FFFFFF; text-align: center; text-decoration: none; padding: 5px;"
                                                                                title="关闭">X</a>
                                                                        </div>
                                                                        <br />
                                                                        <br />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="right">
                                                                        <asp:Button ID="Button1" runat="server" UseSubmitBehavior="false" OnClick="ddpMSbjName_OnSelectedIndexChanged"
                                                                            Text="查 询" />
                                                                        &nbsp;&nbsp;<asp:Button ID="Button2" runat="server" UseSubmitBehavior="false" OnClick="btnMSClear_OnClick"
                                                                            Text="清 空" />&nbsp;&nbsp;
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left" style="width: 98%">
                                                                        <uc3:UserDefinedQueryConditions runat="server" ID="udqMS" QueryColumnsType="TaskView"
                                                                            QueryRows="8" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </asp:Panel>
                                            </td>
                                            <td>
                                                <asp:Button ID="chSubmit" runat="server" Text="变更明细" Visible="false" OnClientClick="return confirm(&quot;确认变更制作明细吗？\r\r提示：\r\r以下所有记录将进行制作明细变更！&quot;);"
                                                    OnClick="chSubmit_Click" />
                                                <asp:Button ID="mssubmit" runat="server" Text="生成制作明细" Visible="false" OnClientClick="return confirm(&quot;确认生成制作明细吗？&quot;);"
                                                    OnClick="mssubmit_Click" />
                                            </td>
                                        </tr>
                                    </table>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                        <asp:Panel ID="NoDataPanel3" runat="server" HorizontalAlign="Center" Font-Size="Large">
                            没有记录!</asp:Panel>
                        <asp:GridView ID="GridView4" OnRowDataBound="GridView4_OnRowDataBound" runat="server"
                            AutoGenerateColumns="False" CssClass="toptable grid" CellPadding="4" ForeColor="#333333"
                            Width="100%">
                            <RowStyle BackColor="#EFF3FB" />
                            <Columns>
                                <asp:TemplateField HeaderText="行号" HeaderStyle-Wrap="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblIndex" runat="server" Text="<%# Convert.ToInt32(Container.DataItemIndex +1) %>" />
                                        <asp:HiddenField ID="hdfMSState" Value='<%#Eval("BM_MSSTATE").ToString() %>' runat="server" />
                                        <asp:HiddenField ID="hdfMSChg" Value='<%#Eval("BM_MSSTATUS").ToString() %>' runat="server" />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="30px" />
                                </asp:TemplateField>
                                <asp:BoundField DataField="BM_TUHAO" HeaderText="图号" HeaderStyle-Wrap="false">
                                    <ItemStyle HorizontalAlign="Left" Wrap="false" />
                                </asp:BoundField>
                                <asp:BoundField DataField="BM_ZONGXU" HeaderText="总序" HeaderStyle-Wrap="false">
                                    <ItemStyle HorizontalAlign="Left" Wrap="false" />
                                </asp:BoundField>
                                <asp:BoundField DataField="BM_CHANAME" HeaderText="名称" HeaderStyle-Wrap="false">
                                    <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                </asp:BoundField>
                                <asp:BoundField DataField="BM_MAGUIGE" HeaderText="材料规格" HeaderStyle-Wrap="false">
                                    <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                </asp:BoundField>
                                <asp:BoundField DataField="BM_MAQUALITY" HeaderText="材质" HeaderStyle-Wrap="false">
                                    <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                </asp:BoundField>
                                <asp:BoundField DataField="NUMBER" HeaderText="单台数量|总数量" HeaderStyle-Wrap="false">
                                    <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                </asp:BoundField>
                                <asp:BoundField DataField="BM_TUUNITWGHT" HeaderText="图纸单重" HeaderStyle-Wrap="false">
                                    <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                </asp:BoundField>
                                <asp:BoundField DataField="BM_TUTOTALWGHT" HeaderText="图纸总重" HeaderStyle-Wrap="false">
                                    <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                </asp:BoundField>
                                <asp:BoundField DataField="BM_MASHAPE" HeaderText="材料种类" HeaderStyle-Wrap="false">
                                    <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                </asp:BoundField>
                                <asp:BoundField DataField="BM_TECHUNIT" HeaderText="单位" HeaderStyle-Wrap="false">
                                    <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                </asp:BoundField>
                                <asp:BoundField DataField="BM_YONGLIANG" HeaderText="材料用量" HeaderStyle-Wrap="false">
                                    <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                </asp:BoundField>
                                <asp:BoundField DataField="BM_MALENGTH" HeaderText="长度" HeaderStyle-Wrap="false">
                                    <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                </asp:BoundField>
                                <asp:BoundField DataField="BM_MAWIDTH" HeaderText="宽度" HeaderStyle-Wrap="false">
                                    <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                </asp:BoundField>
                                <asp:BoundField DataField="BM_NOTE" HeaderText="备注" HeaderStyle-Wrap="false">
                                    <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                </asp:BoundField>
                                <asp:BoundField DataField="BM_XIALIAO" HeaderText="下料方式" HeaderStyle-Wrap="false">
                                    <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                </asp:BoundField>
                                <asp:BoundField DataField="BM_PROCESS" HeaderText="工艺流程" HeaderStyle-Wrap="false">
                                    <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                </asp:BoundField>
                                <asp:BoundField DataField="BM_MATOTALLGTH" DataFormatString="{0:F2}" HeaderText="材料总长(mm)"
                                    HeaderStyle-Wrap="false">
                                    <ItemStyle HorizontalAlign="Center" BackColor="Yellow" Wrap="false" />
                                </asp:BoundField>
                                <asp:BoundField DataField="BM_MABGZMY" DataFormatString="{0:F2}" HeaderText="面域(m2)"
                                    HeaderStyle-Wrap="false">
                                    <ItemStyle HorizontalAlign="Center" BackColor="Yellow" Wrap="false" />
                                </asp:BoundField>
                                <asp:BoundField DataField="BM_MPMY" DataFormatString="{0:F2}" HeaderText="计划面域(m2)"
                                    HeaderStyle-Wrap="false">
                                    <ItemStyle HorizontalAlign="Center" BackColor="Yellow" Wrap="false" />
                                </asp:BoundField>
                                <asp:BoundField DataField="BM_ALLBEIZHU" HeaderText="备注" HeaderStyle-Wrap="false">
                                    <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="变更状态" ItemStyle-HorizontalAlign="Center" HeaderStyle-Wrap="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblMSChangeState" CssClass="notbrk" runat="server" Text='<%#Eval("BM_MSSTATUS").ToString()=="1"?"删除":Eval("BM_MSSTATUS").ToString()=="2"?"增加":Eval("BM_MSSTATUS").ToString()=="3"?"修改":"正常" %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <PagerStyle CssClass="bomcolor" ForeColor="#EEF7FD" HorizontalAlign="Center" />
                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                            <HeaderStyle BackColor="#B9D3EE" Font-Bold="True" ForeColor="#1E5C95" />
                            <EditRowStyle BackColor="#2461BF" />
                            <AlternatingRowStyle BackColor="White" />
                        </asp:GridView>
                        <uc1:UCPaging ID="UCPagingMS" runat="server" />
                        <asp:Panel ID="Panel2" runat="server" Font-Size="Large" BackColor="#B3CDE8">
                            备注：红色标志为变更修改!橙色标志为变更增加!灰色标志为变更删除!<br />
                        </asp:Panel>
                    </ContentTemplate>
                </cc1:TabPanel>
            </cc1:TabContainer>
            <input id="deleteId" type="text" runat="server" value="0" readonly="readonly" style="display: none" />
            <input id="mpid" type="text" runat="server" readonly="readonly" style="display: none" />
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1"
        DisplayAfter="1">
        <ProgressTemplate>
            <div style="position: absolute; top: 70%; right: 40%">
                <table>
                    <tr>
                        <td align="right">
                            <asp:Image ID="Image1" runat="server" ImageUrl="~/Assets/images/ajax-loader2.gif" />
                        </td>
                        <td align="left" style="background-color: Yellow; font-size: medium;">
                            数据处理中，请稍后...
                        </td>
                    </tr>
                </table>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
</asp:Content>
