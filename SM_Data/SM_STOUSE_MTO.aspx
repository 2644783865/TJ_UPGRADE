<%@ Page Language="C#" MasterPageFile="~/Masters/SMBaseMaster.Master" AutoEventWireup="true"
    CodeBehind="SM_STOUSE_MTO.aspx.cs" Inherits="ZCZJ_DPF.SM_Data.SM_STOUSE_MTO"
    Title="MTO调整" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">

    <script language="javascript" type="text/javascript">
     function ShowStoUseModal(ptc,pid) {
            var date=new Date();
            var time=date.getTime();
            var retVal = window.showModalDialog("SM_STOUSE_MTO_STO.aspx?ptc="+ptc+"&&pid="+pid+"&&id="+time, "", "dialogWidth=1000px;dialogHeight=600px;status=no;help=no;scroll=yes");
            if(retVal!=null)
            {
               if(retVal)
               {
                 window.location.reload();
               }
            }
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
    <div style="width: 1000px; margin-right: auto; margin-left: auto">
        <table width="100%">
            <tr>
                <td width="65%" style="color: red">
                    条目没有颜色显示表示未做操作；条目显示红色表示已选择库存；条目显示蓝色表示已调整
                </td>
                <td width="35%">
                    <asp:Button ID="btn_adjust" runat="server" Text="调整" OnClick="btn_adjust_Click" />
                </td>
            </tr>
        </table>
    </div>
    <asp:GridView ID="GridView1" runat="server" EmptyDataText="无数据!" DataKeyNames="ptcode,planno,PUR_ISSTOUSE"
        OnRowDataBound="GridView1_RowDataBound" AutoGenerateColumns="false">
        <Columns>
            <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false">
                <ItemTemplate>
                    <%# Container.DataItemIndex+1%>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="ptcode" HeaderText="计划跟踪号" HeaderStyle-Wrap="false" ItemStyle-Wrap="false"
                ItemStyle-HorizontalAlign="Left" />
            <asp:BoundField DataField="marid" HeaderText="物料编码" HeaderStyle-Wrap="false" ItemStyle-Wrap="false"
                ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="marnm" HeaderText="物料名称" HeaderStyle-Wrap="false" ItemStyle-Wrap="false"
                ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="margg" HeaderText="规格" HeaderStyle-Wrap="false" ItemStyle-Wrap="false"
                ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="marcz" HeaderText="材质" HeaderStyle-Wrap="false" ItemStyle-Wrap="false"
                ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="margb" HeaderText="国标" HeaderStyle-Wrap="false" ItemStyle-Wrap="false"
                ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="length" HeaderText="长" HeaderStyle-Wrap="false" ItemStyle-Wrap="false"
                ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="width" HeaderText="宽" HeaderStyle-Wrap="false" ItemStyle-Wrap="false"
                ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="marunit" HeaderText="单位" HeaderStyle-Wrap="false" ItemStyle-Wrap="false"
                ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="num" HeaderText="计划数量" HeaderStyle-Wrap="false" ItemStyle-Wrap="false"
                ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="usenum" HeaderText="占用数量" HeaderStyle-Wrap="false" ItemStyle-Wrap="false"
                ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="allnote" HeaderText="备注" HeaderStyle-Wrap="false" ItemStyle-Wrap="false"
                ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="pjnm" HeaderText="项目名称" HeaderStyle-Wrap="false" ItemStyle-Wrap="false"
                ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="engnm" HeaderText="工程名称" HeaderStyle-Wrap="false" ItemStyle-Wrap="false"
                ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="engid" HeaderText="生产制号" HeaderStyle-Wrap="false" ItemStyle-Wrap="false"
                ItemStyle-HorizontalAlign="Center" />
        </Columns>
        <HeaderStyle BackColor="#A8B7EC" Font-Bold="True" ForeColor="White" />
        <RowStyle BackColor="#EFF3FB" Height="20px" />
        <AlternatingRowStyle BackColor="White" />
        <PagerStyle CssClass="bomcolor" ForeColor="#EEF7FD" HorizontalAlign="Center" />
    </asp:GridView>
    </div>
</asp:Content>
